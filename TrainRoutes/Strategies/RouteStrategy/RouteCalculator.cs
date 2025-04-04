namespace TrainRoutes.Strategies.RouteStrategy;

public class RouteCalculator
{
    private IRouteStrategy _strategy;

    public RouteCalculator(IRouteStrategy strategy)
    {
        _strategy = strategy;
    }

    /// <summary>
    /// Fetch the distance between 2 towns for a given graph
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="startTown"></param>
    /// <param name="endTown"></param>
    /// <param name="maxStops"></param>
    /// <returns>Returns the shortest distance between two towns</returns>
    public int GetRouteDistance(Graph graph, string startTown, string endTown, int maxStops = int.MaxValue)
    {
        return _strategy.CalculateRoute(graph, startTown, endTown);
    }
}