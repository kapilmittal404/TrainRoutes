using TrainRoutes.Enums;

namespace TrainRoutes.Strategies.TripStrategy;

public class StopBasedTripStrategy : ITripStrategy
{
    public int GetTripCount(Graph graph, string startTown, string endTown, CountCondition countCondition, int conditionValue = Int32.MaxValue)
    {
        int tripCount = 0;
        // Start DFS from the startTown
        if (countCondition == CountCondition.LessThanEqualTo)
        {
            FindTripsWithStopsTillCount(graph, startTown, endTown, conditionValue, currentStops: 0, ref tripCount);
        }
        else if (countCondition == CountCondition.EqualTo)
        {
            FindTripsWithStopsEqualToCount(graph, startTown, endTown, conditionValue, currentStops: 0, ref tripCount);
        }
        
        return tripCount;
    }

    private void FindTripsWithStopsEqualToCount(Graph graph, string currentTown, string endTown, int exactStops, int currentStops, ref int tripCount)
    {
        // If we've reached the exact number of stops, check if we are at the destination
        if (currentStops == exactStops)
        {
            // If the current town is the destination, count the trip
            if (currentTown == endTown)
            {
                tripCount++;
            }
            return; // No need to explore further as we have used up all the allowed stops
        }

        // Explore neighbors recursively, but only if we haven't yet reached the exact number of stops
        foreach (var route in graph.GetRoutesFromTown(currentTown))
        {
            // Recursively visit the next town with an incremented stop count
            FindTripsWithStopsEqualToCount(graph, route.ToTown, endTown, exactStops, currentStops + 1, ref tripCount);
        }
    }

    private void FindTripsWithStopsTillCount(Graph graph, string currentTown, string endTown, int maxStops, int currentStops, ref int tripCount)
    {
        // If we've exceeded the number of stops, return
        if (currentStops > maxStops)
        {
            return;
        }

        // If we have reached the destination and haven't exceeded stops, count the trip
        if (currentTown == endTown && currentStops > 0)  // The path must have at least one stop
        {
            tripCount++;
        }

        // Explore neighbors
        foreach (var route in graph.GetRoutesFromTown(currentTown))
        {
            // Recursively visit the next town
            FindTripsWithStopsTillCount(graph, route.ToTown, endTown, maxStops, currentStops + 1, ref tripCount);
        }
    }
}