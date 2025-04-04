using TrainRoutes.Enums;

namespace TrainRoutes.Strategies.TripStrategy;

public class DistanceBasedTripStrategy : ITripStrategy
{
    public int GetTripCount(Graph graph, string startTown, string endTown, CountCondition countCondition, int conditionValue = Int32.MaxValue)
    {
        int tripCount = 0;
        // Start DFS from the startTown with initial distance 0
        if (countCondition == CountCondition.LessThan)
        {
            FindTripsWithDistanceLessThan(graph, startTown, endTown, conditionValue, 0, ref tripCount);
        }
        
        return tripCount;
    }
    
    private void FindTripsWithDistanceLessThan(Graph graph, string currentTown, string endTown, int maxDistance, int currentDistance, ref int tripCount)
    {
        // If we've exceeded the maximum allowed distance, return
        if (currentDistance >= maxDistance)
        {
            return;
        }

        // If we've reached the destination and the current distance is less than the maxDistance, count the trip
        if (currentTown == endTown && currentDistance > 0)  // Ensure it's not the starting point
        {
            tripCount++;
        }

        // Explore neighbors recursively
        foreach (var route in graph.GetRoutesFromTown(currentTown))
        {
            // Recursively visit the next town with the updated distance
            FindTripsWithDistanceLessThan(graph, route.ToTown, endTown, maxDistance, currentDistance + route.Distance, ref tripCount);
        }
    }
}