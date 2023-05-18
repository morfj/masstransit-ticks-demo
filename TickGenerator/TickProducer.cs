using System;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace TickGenerator
{
    public class TickProducer : BackgroundService
    {
        private readonly IBus _bus;

        public TickProducer(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var tickEvent = new TickEvent(DateTime.Now);
                await _bus.Publish(tickEvent, cancellationToken);
                Console.WriteLine($"New event: {tickEvent}");
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}