using System;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Domain.Entities;

namespace TrainRoutes.Domain.Services
{
    public class RouteCalculator : IRouteCalculator
    {
        private readonly TownGraph _graph;

        public RouteCalculator(TownGraph graph)
        {
            _graph = graph;
        }

        public int? CalculateRouteDistance(IEnumerable<string> route)
        {
            int totalDistance = 0;
            var towns = route.ToList();

            for (int i = 0; i < towns.Count - 1; i++)
            {
                string from = towns[i];
                string to = towns[i + 1];

                // Check if the starting town exists in the graph
                if (!_graph.AdjacencyList.ContainsKey(from))
                {
                    Console.WriteLine($"DEBUG: Town '{from}' does not exist in the graph.");
                    return null; // Route doesn't exist
                }

                // Check if there is a direct connection to the next town
                var connection = _graph.AdjacencyList[from]?.FirstOrDefault(c => c.Item1 == to);
                if (connection == null || connection.Value.Item2 <= 0)
                {
                    Console.WriteLine($"DEBUG: No valid connection from '{from}' to '{to}'.");
                    return null; // Route doesn't exist
                }

                Console.WriteLine($"DEBUG: Connection found from '{from}' to '{to}' with distance {connection.Value.Item2}.");
                totalDistance += connection.Value.Item2;
            }

            return totalDistance;
        }

        public int FindShortestRoute(string start, string end)
        {
            var visited = new HashSet<string>();
            return FindShortestRouteHelper(start, end, visited, 0, int.MaxValue);
        }

        private int FindShortestRouteHelper(string current, string end, HashSet<string> visited, int currentDistance, int shortestDistance)
        {
            if (current == end && visited.Count > 0)
            {
                return Math.Min(shortestDistance, currentDistance);
            }

            visited.Add(current);

            foreach (var (neighbor, distance) in _graph.AdjacencyList[current])
            {
                if (!visited.Contains(neighbor) || neighbor == end)
                {
                    shortestDistance = FindShortestRouteHelper(neighbor, end, visited, currentDistance + distance, shortestDistance);
                }
            }

            visited.Remove(current);
            return shortestDistance;
        }

        public int CountTrips(string start, string end, int maxStops, bool exactStops = false)
        {
            return CountTripsHelper(start, end, 0, maxStops, exactStops);
        }

        private int CountTripsHelper(string current, string end, int stops, int maxStops, bool exactStops)
        {
            if (stops > maxStops) return 0;

            int count = 0;
            if (current == end && stops > 0 && (!exactStops || stops == maxStops))
            {
                count++;
            }

            foreach (var (neighbor, _) in _graph.AdjacencyList[current])
            {
                count += CountTripsHelper(neighbor, end, stops + 1, maxStops, exactStops);
            }

            return count;
        }

        public int CountTripsByDistance(string start, string end, int maxDistance)
        {
            return CountTripsByDistanceHelper(start, end, 0, maxDistance);
        }

        private int CountTripsByDistanceHelper(string current, string end, int currentDistance, int maxDistance)
        {
            if (currentDistance >= maxDistance) return 0;

            int count = 0;
            if (current == end && currentDistance > 0)
            {
                count++;
            }

            foreach (var (neighbor, distance) in _graph.AdjacencyList[current])
            {
                count += CountTripsByDistanceHelper(neighbor, end, currentDistance + distance, maxDistance);
            }

            return count;
        }
    }
}
