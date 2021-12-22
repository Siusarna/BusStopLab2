using BusStop.Entities;

namespace BusStop.Messages
{
    public class RequestAccepted
    {
        public Bus Bus { get; }
        public string StopId { get; }

        public RequestAccepted(Bus bus, string stopId)
        {
            Bus = bus;
            StopId = stopId;
        }
    }
}