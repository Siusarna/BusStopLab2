using BusStop.Entities;

namespace BusStop.Messages
{
    public class LeaveBusStop
    {
        public Bus Bus { get; }

        public LeaveBusStop(Bus bus)
        {
            Bus = bus;
        }
    }
}