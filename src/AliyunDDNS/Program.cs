using System;

namespace AliyunDDNS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var log = new ConsoleLog("AliyunDDNS")) {
                log.Info($"aliyun ddns v{typeof(Program).Assembly.GetName().Version} by symbolspace@outlook.com");
                log.Info("");

                // create ddns instance
                //   default config : ~/aliyun.ddns.config.json
                var ddns = new module.AliyunDDNS.DomainIPSyncService();
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
