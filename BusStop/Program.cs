using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using BusStop.Actors;

namespace BusStop
{
    class Program
    {
        static void Main(string[] args)
        {
            var busesNames = new List<string>();
            for (int i = 0; i < 16; i++)
            {
                busesNames.Add("bus" + i);
            }

            using var system = ActorSystem.Create("test");
            var busStop = system.ActorOf<BusStopActor>("stop");

            Parallel.ForEach(busesNames, busName =>
            {
                system.ActorOf(Props.Create(() => new BusActor(busName, busStop)));
            });

            Console.ReadLine();
        }
    }
}