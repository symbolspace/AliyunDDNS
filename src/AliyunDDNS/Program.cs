using System;

namespace AliyunDDNS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var log = new ConsoleLog("AliyunDDNS")) {
                log.Info($"aliyun ddns v{typeof(Program).Assembly.GetName().Version} by symbolspace@outlook.com");

                // create ddns instance
                //   default config : ~/aliyun.ddns.config.json
                string configFile = "/app/aliyun.ddns.config.json";
                if (!System.IO.File.Exists(configFile))
                    configFile = "~/aliyun.ddns.config.json";
                log.Info($"   config:{configFile}");
                log.Info("");
                string json = AppHelper.LoadTextFile(configFile, System.Text.Encoding.UTF8);
                log.Info(json);
                var config = JSON.ToObject<module.AliyunDDNS.Config>(json, true);
                log.Info(JSON.ToNiceJSON(config));

                var ddns = new module.AliyunDDNS.DomainIPSyncService(config);
                // set log 
                ddns.Log = log;
                // start task
                ddns.Start();

                System.Console.ReadLine();
                ddns.Stop();
            }
        }
    }
}
