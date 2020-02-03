using System.Collections.Generic;

namespace module.AliyunDDNS {

    /// <summary>
    /// domain info
    /// </summary>
    public class DomainInfo {
        /// <summary>
        /// domain
        /// </summary>
        public string domain { get; set; }
        /// <summary>
        /// dns provider, fixed value 'aliyun'
        /// </summary>
        public string provider { get; set; }
        /// <summary>
        /// accessKey info
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// sub domain list
        /// </summary>
        public List<SubDomainInfo> items { get; set; } = new List<SubDomainInfo>();
    }
}
