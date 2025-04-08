using System.IO;
using TrainRoutes.Domain.Entities;
using TrainRoutes.Domain.Interfaces;

namespace TrainRoutes.Infrastructure.Repositories
{
    public class GraphRepository : IGraphRepository
    {
        public TownGraph LoadGraph(string filePath)
        {
            var graph = new TownGraph();

            // Example adjacency list setup
            graph.AdjacencyList["A"] = new List<(string, int)> { ("B", 5), ("D", 5), ("E", 7) };
            graph.AdjacencyList["B"] = new List<(string, int)> { ("C", 4) };
            graph.AdjacencyList["C"] = new List<(string, int)> { ("D", 8), ("E", 2) };
            graph.AdjacencyList["D"] = new List<(string, int)> { ("C", 8), ("E", 6) };
            graph.AdjacencyList["E"] = new List<(string, int)> { ("B", 3) }; //invalid connection to "D"

            return graph;
        }
    }
}
