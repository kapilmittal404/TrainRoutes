using System.Collections.Generic;
using TrainRoutes.Domain.Entities;
using TrainRoutes.Domain.Services;
using Xunit;

namespace TrainRoutes.Tests
{
    public class RouteCalculatorTests
    {
        private readonly RouteCalculator _routeCalculator;

        public RouteCalculatorTests()
        {
            // Initialize the graph with routes
            var graph = new TownGraph();
            graph.AddRoute("A", "B", 5);
            graph.AddRoute("B", "C", 4);
            graph.AddRoute("C", "D", 8);
            graph.AddRoute("D", "C", 8);
            graph.AddRoute("D", "E", 6);
            graph.AddRoute("A", "D", 5);
            graph.AddRoute("C", "E", 2);
            graph.AddRoute("E", "B", 3);
            graph.AddRoute("A", "E", 7);

            _routeCalculator = new RouteCalculator(graph);
        }

        [Fact]
        public void Test1_DistanceOfRouteABC_Is9()
        {
            var distance = _routeCalculator.CalculateRouteDistance(new List<string> { "A", "B", "C" });
            Assert.Equal(9, distance);
        }

        [Fact]
        public void Test2_DistanceOfRouteAD_Is5()
        {
            var distance = _routeCalculator.CalculateRouteDistance(new List<string> { "A", "D" });
            Assert.Equal(5, distance);
        }

        [Fact]
        public void Test3_DistanceOfRouteADC_Is13()
        {
            var distance = _routeCalculator.CalculateRouteDistance(new List<string> { "A", "D", "C" });
            Assert.Equal(13, distance);
        }

        [Fact]
        public void Test4_DistanceOfRouteAEBIs22()
        {
            var distance = _routeCalculator.CalculateRouteDistance(new List<string> { "A", "E", "B", "C", "D" });
            Assert.Equal(22, distance);
        }

        [Fact]
        public void Test5_RouteAED_DoesNotExist()
        {
            var distance = _routeCalculator.CalculateRouteDistance(new List<string> { "A", "E", "D" });
            Assert.Null(distance);
        }

        [Fact]
        public void Test6_NumberOfTripsFromCToCWithMax3Stops_Is2()
        {
            var trips = _routeCalculator.CountTrips("C", "C", 3);
            Assert.Equal(2, trips);
        }

        [Fact]
        public void Test7_NumberOfTripsFromAToCWithExactly4Stops_Is3()
        {
            var trips = _routeCalculator.CountTrips("A", "C", 4, exactStops: true);
            Assert.Equal(3, trips);
        }

        [Fact]
        public void Test8_ShortestRouteFromAToC_Is9()
        {
            var shortestDistance = _routeCalculator.FindShortestRoute("A", "C");
            Assert.Equal(9, shortestDistance);
        }

        [Fact]
        public void Test9_ShortestRouteFromBToB_Is9()
        {
            var shortestDistance = _routeCalculator.FindShortestRoute("B", "B");
            Assert.Equal(9, shortestDistance);
        }

        [Fact]
        public void Test10_NumberOfTripsFromCToCWithDistanceLessThan30_Is7()
        {
            var trips = _routeCalculator.CountTripsByDistance("C", "C", 30);
            Assert.Equal(7, trips);
        }
    }
}
