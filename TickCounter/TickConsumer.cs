using System;
using System.Threading.Tasks;
using Contract;
using MassTransit;

namespace TickCounter
{
    public class TickConsumer : IConsumer<TickEvent>
    {
        private static int _count;

        public Task Consume(ConsumeContext<TickEvent> context)
        {
            _count++;
            Console.WriteLine($"TickConsumer:Consume() [{_count:000} - {DateTime.Now.TimeOfDay}]: {context.Message.Timestamp}");
            return Task.CompletedTask;
        }
    }
}