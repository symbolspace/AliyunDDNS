using System.Collections.Generic;

namespace module.AliyunDDNS {

    /// <summary>
    /// config
    /// </summary>
    public class Config {
        /// <summary>
        /// check interval, seconds, default 10
        /// </summary>
        public int interval { get; set; } = 10;
        /// <summary>
        /// show public ip url
        /// </summary>
        public string showIPUrl { get; set; }
        /// <summary>
        /// accessKey map
        /// </summary>
        public Dictionary<string, AccessKeyInfo> accessKeys { get; set; }
        /// <summary>
        /// domain map
        /// </summary>
        public Dictionary<string, DomainInfo> domains { get; set; }
    }
}
