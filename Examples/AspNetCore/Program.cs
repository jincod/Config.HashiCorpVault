using Config.HashiCorpVault;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var config = builder.Build();

                    builder.AddHashiCorpVault(
                        config["Vault:Url"],
                        config["Vault:Token"],
                        config["Vault:Path"],
                        true
                    );
                })
                .UseStartup<Startup>();
        }
    }
}
