using BusStop.Entities;

namespace BusStop.Messages
{
    public class RequestBusStop
    {
        public Bus Bus { get; }

        public RequestBusStop(Bus bus)
        {
            Bus = bus;
        }
    }
}