using MassTransit;

namespace TickCounter
{
    public class TickConsumerDefinition : ConsumerDefinition<TickConsumer>
    {
        public TickConsumerDefinition()
        {
            EndpointName = "tick-counter";
            ConcurrentMessageLimit = 1;
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<TickConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
        }
    }
}
