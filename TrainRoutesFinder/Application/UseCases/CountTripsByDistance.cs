using TrainRoutes.Domain.Services;

namespace TrainRoutes.Application.UseCases
{
    public class CountTripsByDistance
    {
        private readonly IRouteCalculator _routeCalculator;

        public CountTripsByDistance(IRouteCalculator routeCalculator)
        {
            _routeCalculator = routeCalculator;
        }

        public int Execute(string start, string end, int maxDistance)
        {
            return _routeCalculator.CountTripsByDistance(start, end, maxDistance);
        }
    }
}
