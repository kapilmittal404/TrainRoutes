using System.Collections.Generic;

namespace TrainRoutes.Domain.Entities
{
    public class TownGraph
    {
        public Dictionary<string, List<(string, int)>> AdjacencyList { get; } = new();

        public void AddRoute(string from, string to, int distance)
        {
            if (distance <= 0) throw new ArgumentException("Distance must be greater than zero.");

            if (!AdjacencyList.ContainsKey(from))
            {
                AdjacencyList[from] = new List<(string, int)>();
            }
            AdjacencyList[from].Add((to, distance));
        }


    }
}
