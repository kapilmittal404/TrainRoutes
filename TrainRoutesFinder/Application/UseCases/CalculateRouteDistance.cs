using System.Collections.Generic;
using TrainRoutes.Domain.Entities;
using TrainRoutes.Domain.Services;

namespace TrainRoutes.Application.UseCases
{
    public class CalculateRouteDistance
    {
        private readonly IRouteCalculator _routeCalculator;

        public CalculateRouteDistance(IRouteCalculator routeCalculator)
        {
            _routeCalculator = routeCalculator;
        }

        public int? Execute(IEnumerable<string> route)
        {
            return _routeCalculator.CalculateRouteDistance(route);
        }
    }
}
