using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.IO;
using Akka.Routing;
using BusStop.Entities;
using BusStop.Messages;

namespace BusStop.Actors
{
    public class BusStopActor : ReceiveActor
    {
        private readonly List<BusStopModel> _busStopModels;
        private readonly List<AwaitingBus> _awaitingBuses;
        public BusStopActor()
        {
            _busStopModels = new List<BusStopModel>()
            {
                new BusStopModel(5, "1"),
                new BusStopModel(2, "2"),
                new BusStopModel(3, "3")
            };
            _awaitingBuses = new List<AwaitingBus>();
            Receive<RequestBusStop>(m =>
            {
                var availableStop = _busStopModels.FirstOrDefault(x => x.Count < x.MaxBuses);
                if (availableStop != null)
                {
                    availableStop.AddBus(m.Bus);
                    Sender.Tell(new RequestAccepted(m.Bus, availableStop.Id));
                }
                else
                {
                    _awaitingBuses.Add(new AwaitingBus(Sender, m.Bus));
                    Sender.Tell(new WaitingForFreeStops(m.Bus));
                }
            });
            Receive<LeaveBusStop>(m =>
            {
                var busStop = _busStopModels.FirstOrDefault(x => x.Contains(m.Bus));
                busStop.RemoveBus(m.Bus);

                var bus = _awaitingBuses.FirstOrDefault();
                if (bus != null)
                {
                    var availableStop = _busStopModels.FirstOrDefault(x => x.Count < x.MaxBuses);
                    _awaitingBuses.Remove(bus);
                    availableStop.AddBus(bus.Bus);
                    bus.Source.Tell(new RequestAccepted(bus.Bus, availableStop.Id));
                }
            });
        }
    }
}