using System;
using System.Security.Cryptography;
using System.Threading;
using Akka.Actor;
using BusStop.Entities;
using BusStop.Messages;

namespace BusStop.Actors
{
    public class BusActor : ReceiveActor
    {
        public Bus Bus { get; }
        private int _stopTime;
        private IActorRef _busStop;

        public BusActor(string busId, IActorRef busStop)
        {
            Bus = new Bus(busId);
            _stopTime = RandomNumberGenerator.GetInt32(500, 3000);
            _busStop = busStop;
            Receive<RequestAccepted>(m =>
            {
                Console.WriteLine($"Bus {m.Bus.Id} on stop {m.StopId}. Will left after {_stopTime}");
                Thread.Sleep(_stopTime);
                Console.WriteLine($"Bus {m.Bus.Id} has left {m.StopId}.");
                Sender.Tell(new LeaveBusStop(m.Bus));
            });
            Receive<WaitingForFreeStops>(m =>
            {
                Console.WriteLine($"Bus {m.Bus.Id} is waiting for free stops.");
            });
            
            GoToStop();
        }

        public void GoToStop()
        {
            Console.WriteLine($"Bus {Bus.Id} going to the bus stop");
            _busStop.Tell(new RequestBusStop(Bus));
        }
    }
}