namespace module.AliyunDDNS {

    class AliyunCore {
        public static Aliyun.Acs.Core.IAcsClient CreateClient(string regionId, string accessKeyId, string accessKeySecret) {
            bool retry = false;
        lb_Retry:
            try {
                Aliyun.Acs.Core.Profile.IClientProfile profile = Aliyun.Acs.Core.Profile.DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
                return new Aliyun.Acs.Core.DefaultAcsClient(profile);
            } catch {
                if (!retry) {
                    retry = true;
                    Aliyun.Acs.Core.Profile.DefaultProfile.GetProfile().AddEndpoint("cn-hangzhou", "cn-hangzhou", "Alidns", "alidns.aliyuncs.com");
                    goto lb_Retry;
                }
                return null;
            }
        }
    }

}
