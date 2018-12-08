namespace TestOkur.TestHelper
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Reflection;

    public abstract class TestServerFactory<TStartup>
        where TStartup : class
    {
        private static readonly string ContentRoot = ProjectPathFinder.GetPath("src", typeof(TStartup));

        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(ContentRoot)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile("appsettings.Development.json", true, false)
                .AddEnvironmentVariables()
                .Build();

        public virtual TestServer Create(Action<IServiceCollection> configureServices = null)
        {
            var webHostBuilder = CreateWebHostBuilder(null);
            webHostBuilder.UseEnvironment("Development");
            webHostBuilder.UseConfiguration(Configuration);
            webHostBuilder.UseStartup<TStartup>();

            if (configureServices != null)
            {
                webHostBuilder.ConfigureServices(configureServices);
            }

            var testServer = new TestServer(webHostBuilder);

            return testServer;
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(ContentRoot)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                    config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                });

            if (args != null)
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            }

            return builder;
        }
    }
}
