using TrainRoutes.Domain.Services;

namespace TrainRoutes.Application.UseCases
{
    public class CountTrips
    {
        private readonly IRouteCalculator _routeCalculator;

        public CountTrips(IRouteCalculator routeCalculator)
        {
            _routeCalculator = routeCalculator;
        }

        public int Execute(string start, string end, int stops, bool exactStops = false)
        {
            return _routeCalculator.CountTrips(start, end, stops, exactStops);
        }
    }
}
