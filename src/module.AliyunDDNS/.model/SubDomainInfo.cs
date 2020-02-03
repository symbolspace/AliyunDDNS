namespace module.AliyunDDNS {

    /// <summary>
    /// sub domain info
    /// </summary>
    public class SubDomainInfo {
        /// <summary>
        /// record type, fixed value : 'A'
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// record ttl, default 100
        /// </summary>
        public int ttl { get; set; }
    }
}
