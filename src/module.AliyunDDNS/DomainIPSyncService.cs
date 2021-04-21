using System;
using System.Collections.Generic;
using Symbol.Collections.Generic;

namespace module.AliyunDDNS {

    /// <summary>
    /// domain ip sync service.
    /// </summary>
    public class DomainIPSyncService {

        #region fields
        private Config _config;
        private ILog _log;
        private System.Timers.Timer _timer;
        private NameValueCollection<AliyunDNS> _list_dns;
        private string _publicIP;
        #endregion

        #region properties
        /// <summary>
        /// get config object.
        /// </summary>
        public Config Config { get { return _config; } }

        /// <summary>
        /// get or set log object.
        /// </summary>
        public ILog Log {
            get { return _log ?? LogBase.Empty; }
            set { _log = value; }
        }


        #endregion

        #region ctor
        /// <summary>
        /// ctor("~/aliyun.ddns.config.json")
        /// </summary>
        public DomainIPSyncService()
            : this("~/aliyun.ddns.config.json") {

        }
        /// <summary>
        /// ctor(config)
        /// </summary>
        /// <param name="config">config object, FastObject instance</param>
        public DomainIPSyncService(FastObject config)
            : this(JSON.ToObject<Config>(config.ToJson())) {
        }
        /// <summary>
        /// ctor(config)
        /// </summary>
        /// <param name="config">config object, FastObject instance</param>
        public DomainIPSyncService(Config config) {
            _list_dns = new NameValueCollection<AliyunDNS>();
            _config = config;
            foreach (var item in _config.domains) {
                item.Value.domain = item.Key;
            }
        }
        #endregion

        #region methods

        #region Start
        /// <summary>
        /// start task.
        /// </summary>
        public void Start() {
            // timer interval , second, default 10
            int interval = _config.interval;
            if (interval < 1)
                interval = 10;
            _timer = new System.Timers.Timer();
            _timer.Interval = interval * 1000;
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            if (interval > 20) {
                ThreadHelper.Delay(1000, DoWork);
            }
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            DoWork();
        }
        #endregion

        #region Stop
        /// <summary>
        /// stop task.
        /// </summary>
        public void Stop() {
            if (_timer == null)
                return;
            _timer.Elapsed -= _timer_Elapsed;
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }
        #endregion

        #region DoWork
        void DoWork() {
            _timer?.Stop();
            try {
                DoWorkBody();
            } catch (Exception error) {
                Log?.Error(error);
            } finally {
                GC.Collect();
                _timer?.Start();
            }
        }
        void DoWorkBody() {
            Log?.Info("work begin ...");
            string publicIP = GetPublicIP();
            if (string.IsNullOrEmpty(publicIP)) {
                Log?.Warning("  public ip is null.");
                return;
            }
            _publicIP = publicIP;
            foreach (var item in _config.domains.Values) {
                DoWork_Domain(item);
            }
            Log?.Info("work end.");
        }
        void DoWork_Domain(DomainInfo domainInfo) {
            Log?.Info($"  >domain: {domainInfo.domain} ->{domainInfo.provider}");
            var dns = CreateAliyunDNS(domainInfo.accessKey);
            if (dns == null) {
                Log?.Warning($"    create dns fail, accessKeys not found. accessKey={domainInfo.accessKey}");
                return;
            }
            foreach (var item in domainInfo.items) {
                Log?.Info($"    >{item.name} A  {_publicIP} ... ");
                if (dns.ModifyA(domainInfo.domain, item.name, _publicIP)) {
                    Log?.Info("      modify success");
                }else{
                    if (dns.AddA(domainInfo.domain, item.name, _publicIP, item.ttl)) {
                        Log?.Info("      add success");
                    } else {
                        Log?.Error("      add/modify fail");
                    }
                }
            }
        }
        AliyunDNS CreateAliyunDNS(string accessKey) {
            var result = _list_dns[accessKey];
            if (result == null) {
                AccessKeyInfo accessKeyInfo;
                if (!_config.accessKeys.TryGetValue(accessKey, out accessKeyInfo)) {
                    return null;
                }
                result = new AliyunDNS(accessKeyInfo.accessKeyId, accessKeyInfo.accessKeySecret);
                _list_dns[accessKey] = result;
            }
            result.Log = Log;
            return result;
        }

        #endregion

        #region GetPublicIP
        string GetPublicIP() {
            string url = _config.showIPUrl;
            if (string.IsNullOrEmpty(url)) {
                Log?.Warning("get public ip : config.showIPUrl missing.");
                return null;
            }
            Log?.Info($"get public ip from '{url}'");
            string html = null;
            try {
                using (Symbol.Net.Downloader downloader = new Symbol.Net.Downloader()) {
                    downloader.RetryCount = 0;
                    downloader.Encoding = System.Text.Encoding.UTF8;
                    html = downloader.DownloadString(url);
                    Log?.Info($"get public ip response:{html}");
                    if (html != null && html.StartsWith("{")) {
                        html = JSON.Parse(html).Path("ip").Convert<string>();
                    }
                }
            } catch (Exception error) {
                Log?.Error(error);
            }
            try {
                if (System.Net.IPAddress.Parse(html) != null)
                    return html;
            } catch (Exception error) {
                Log?.Error(error);
            }
            return null;
        }
        #endregion


        #endregion

    }
}
