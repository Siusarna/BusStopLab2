using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Routing;

namespace BusStop.Entities
{
    public class BusStopModel
    {
        public int MaxBuses { get; }
        public string Id { get; }
        private readonly List<Bus> _buses;

        public int Count => _buses.Count;

        public BusStopModel(int maxBuses, string id)
        {
            MaxBuses = maxBuses;
            Id = id;
            _buses = new List<Bus>();
        }

        public void AddBus(Bus bus)
        {
            if (Count >= MaxBuses)
            {
                throw new ArgumentOutOfRangeException();
            }
            _buses.Add(bus);
        }

        public void RemoveBus(Bus bus)
        {
            _buses.Remove(_buses.FirstOrDefault(x => x.Id == bus.Id));
        }

        public bool Contains(Bus bus)
        {
            return _buses.Any(x => x.Id == bus.Id);
        }
    }
}