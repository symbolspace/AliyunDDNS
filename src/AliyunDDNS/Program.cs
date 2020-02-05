//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AliyunDDNS {
    class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureServices((serviceProvider) => {
                     //var log = new ConsoleLog("AliyunDDNS");
                     //log.Info($"aliyun ddns v{typeof(module.AliyunDDNS.DomainIPSyncService).Assembly.GetName().Version} by symbolspace@outlook.com");
                     //serviceProvider.AddSingleton<ILog>(log);

                     string configFile = "/app/aliyun.ddns.config.json";
                     if (!System.IO.File.Exists(configFile))
                         configFile = "~/aliyun.ddns.config.json";
                     //log.Info($"   config:{configFile}");
                     var ddns = new module.AliyunDDNS.DomainIPSyncService(configFile);
                     //ddns.Log = log;
                     serviceProvider.AddSingleton(ddns);
                     ddns.Start();
                 });
        }

    }
}
