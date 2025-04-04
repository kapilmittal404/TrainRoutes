using TrainRoutes.Enums;

namespace TrainRoutes.Strategies.TripStrategy;

public class TripCalculator
{
    private ITripStrategy _strategy;

    public TripCalculator(ITripStrategy strategy)
    {
        _strategy = strategy;
    }

    public int GetTripCount(Graph graph, string startTown, string endTown, int conditionValue = int.MaxValue, CountCondition countCondition = CountCondition.EqualTo)
    {
        return _strategy.GetTripCount(graph, startTown, endTown, countCondition, conditionValue);
    }
}