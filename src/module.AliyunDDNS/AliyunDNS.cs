using System;

namespace module.AliyunDDNS {

    class AliyunDNS {
        #region fields
        private Aliyun.Acs.Core.IAcsClient _client;
        #endregion

        #region ctor
        public AliyunDNS(string accessKeyId, string accessKeySecret) : this(AliyunCore.CreateClient("cn-hangzhou", accessKeyId, accessKeySecret)) {
        }
        public AliyunDNS(Aliyun.Acs.Core.IAcsClient client) {
            _client = client;
        }
        #endregion

        #region methods

        #region 
        public bool AddA(string domain, string name, string ip, int ttl = 600) {
            //Console.WriteLine($"     {name}\t{ip}");
            var request = new Aliyun.Acs.Alidns.Model.V20150109.AddDomainRecordRequest() {
                RR = name,
                Type = "A",
                _Value = ip,
                TTL = ttl,
                //Priority = item.Priority,
                //Line = item.Line,
                Priority = 1,
                DomainName = domain,
            };
            var response = AddDomainRecord(request);
            Console.WriteLine($"        =>{response?.HttpResponse.Status}   recordId={response?.RecordId}");
            return response != null && response.HttpResponse.Status == 200;
        }
        public bool ModifyA(string domain, string name, string ip) {
            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ip))
                return false;
            //Console.WriteLine($"     {name}\t{ip}");
            var records = DescribeDomainRecords(new Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainRecordsRequest() {
                DomainName = domain,
            });
            if (records == null || records.DomainRecords.Count == 0) {

                return false;
            }
            var item = records.DomainRecords.Find(p => string.Equals(p.RR, name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
                return false;
            if (string.Equals(item._Value, ip, StringComparison.OrdinalIgnoreCase))
                return true;
            var request = new Aliyun.Acs.Alidns.Model.V20150109.UpdateDomainRecordRequest() {
                RecordId = item.RecordId,
                RR = item.RR,
                Type = item.Type,
                _Value = ip,
                TTL = item.TTL,
                Priority = item.Priority,
                Line = item.Line,
            };
            if (request.Priority == null)
                request.Priority = 1;
            var response = UpdateDomainRecord(request);
            Console.WriteLine($"        =>{response?.HttpResponse.Status}   recordId={response?.RecordId}");
            return response != null && response.HttpResponse.Status == 200;
        }
        #endregion

        #region DescribeDomainRecords
        public Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainRecordsResponse DescribeDomainRecords(Aliyun.Acs.Alidns.Model.V20150109.DescribeDomainRecordsRequest request) {
            try {
                return _client.GetAcsResponse(request);
            } catch {
                return null;
            }
        }
        #endregion
        #region DeleteDomainRecord
        public Aliyun.Acs.Alidns.Model.V20150109.DeleteDomainRecordResponse DeleteDomainRecord(Aliyun.Acs.Alidns.Model.V20150109.DeleteDomainRecordRequest request) {
            try {
                return _client.GetAcsResponse(request);
            } catch {
                return null;
            }
        }
        #endregion
        #region AddDomainRecord
        public Aliyun.Acs.Alidns.Model.V20150109.AddDomainRecordResponse AddDomainRecord(Aliyun.Acs.Alidns.Model.V20150109.AddDomainRecordRequest request) {
            try {
                return _client.GetAcsResponse(request);
            } catch {
                return null;
            }
        }
        #endregion
        #region UpdateDomainRecord
        public Aliyun.Acs.Alidns.Model.V20150109.UpdateDomainRecordResponse UpdateDomainRecord(Aliyun.Acs.Alidns.Model.V20150109.UpdateDomainRecordRequest request) {
            try {
                return _client.GetAcsResponse(request);
            } catch {
                return null;
            }
        }
        #endregion

        #endregion
    }


}
