namespace TrainRoutes.Domain.Services
{
    public interface IRouteCalculator
    {
        int ? CalculateRouteDistance(IEnumerable<string> route);
        int FindShortestRoute(string start, string end);
        int CountTrips(string start, string end, int maxStops, bool exactStops = false);
        int CountTripsByDistance(string start, string end, int maxDistance);
    }
}
