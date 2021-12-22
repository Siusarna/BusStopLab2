using BusStop.Entities;

namespace BusStop.Messages
{
    public class WaitingForFreeStops
    {
        public Bus Bus { get; }

        public WaitingForFreeStops(Bus bus)
        {
            Bus = bus;
        }
    }
}