using TrainRoutes.Enums;

namespace TrainRoutes.Strategies.TripStrategy;

public interface ITripStrategy
{
    int GetTripCount(Graph graph, string startTown, string endTown, CountCondition countCondition, int conditionValue = int.MaxValue);
}