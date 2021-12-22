using Akka.Actor;

namespace BusStop.Entities
{
    public class AwaitingBus
    {
        public IActorRef Source { get; }
        public Bus Bus { get; }

        public AwaitingBus(IActorRef source, Bus bus)
        {
            Source = source;
            Bus = bus;
        }
    }
}