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
                        busCfg.UsingRabbitMq((_, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ReceiveEndpoint("tick-counter", re =>
                            {
                                re.ConfigureConsumeTopology = false;
                                re.SetQueueArgument("declare", "lazy");
                                re.Consumer<TickConsumer>(configurator => configurator.UseConcurrentMessageLimit(1));
                                re.Bind("tick-event", e =>
                                {
                                    e.ExchangeType = "fanout";
                                });
                            });
                        });
                    });
                });
    }
}
