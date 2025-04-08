using TrainRoutes.Domain.Services;

namespace TrainRoutes.Application.UseCases
{
    public class FindShortestRoute
    {
        private readonly IRouteCalculator _routeCalculator;

        public FindShortestRoute(IRouteCalculator routeCalculator)
        {
            _routeCalculator = routeCalculator;
        }

        public int Execute(string start, string end)
        {
            return _routeCalculator.FindShortestRoute(start, end);
        }
    }
}
