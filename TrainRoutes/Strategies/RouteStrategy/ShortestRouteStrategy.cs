namespace TrainRoutes.Strategies.RouteStrategy;

public class ShortestRouteStrategy : IRouteStrategy
{
    /// <summary>
    /// Implement Dijkstra's Algorithm to find the shortest path
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="startTown"></param>
    /// <param name="endTown"></param>
    /// <returns>The shortest distance from startTown to endTown</returns>
    public int CalculateRoute(Graph graph, string startTown, string endTown)
    {
        // Dictionary to store the shortest distance to each town
        var distances = new Dictionary<string, int>();
        
        // Priority queue (min-heap) to store towns with their current shortest distance
        var priorityQueue = new PriorityQueue<string, int>();

        // Initialize distances
        foreach (var town in graph.GetAllTowns())
        {
            distances[town] = int.MaxValue;  // Set all distances to infinity
        }
        
        // Distance to the start town is 0
        distances[startTown] = 0;
        
        // Add the start town to the priority queue with distance 0
        priorityQueue.Enqueue(startTown, 0);

        // Track the shortest path
        var previousTowns = new Dictionary<string, string>();

        while (priorityQueue.Count > 0)
        {
            // Get the town with the smallest distance
            var currentTown = priorityQueue.Dequeue();

            // If we reached the destination, stop
            if (currentTown == endTown && previousTowns.Count != 0)
            {
                break;
            }

            // Get all the neighboring towns and their distances
            foreach (var route in graph.GetRoutesFromTown(currentTown))
            {
                var neighbor = route.ToTown;
                var newDistance = distances[currentTown] + route.Distance;

                // If a shorter path to the neighbor is found, update it
                if (newDistance < distances[neighbor] || distances[neighbor] == 0) // The second OR condition makes it travel around even if start and end are given the same
                {
                    distances[neighbor] = newDistance;
                    previousTowns[neighbor] = currentTown;
                    priorityQueue.Enqueue(neighbor, newDistance);
                }
            }
        }

        // If no path is found to the end town, return -1 or some indication that no path exists
        if (distances[endTown] == int.MaxValue)
        {
            return -1;
        }

        // Return the shortest distance to the end town
        return distances[endTown];
    }
}