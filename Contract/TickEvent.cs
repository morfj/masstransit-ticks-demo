using System;
using MassTransit;

namespace Contract
{
    [EntityName("tick-event")]
    public record TickEvent(DateTime Timestamp);
}
