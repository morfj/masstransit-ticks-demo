using System.Threading.Tasks;
using Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace TickGenerator
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(busCfg =>
                    {
                        busCfg.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.Publish<TickEvent>(x =>
                            {
                                x.ExchangeType = "fanout";
                            });
                        });
                    });

                    services.AddHostedService<TickProducer>();
                });
    }
}
