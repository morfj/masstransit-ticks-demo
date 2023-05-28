using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace TickCounter
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddMassTransit(busCfg =>
                    {
                        busCfg.AddConsumer(typeof(TickConsumer), typeof(TickConsumerDefinition));
                        busCfg.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
