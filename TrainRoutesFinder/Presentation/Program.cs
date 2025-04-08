using TrainRoutes.Application.UseCases;
using TrainRoutes.Domain.Services;
using TrainRoutes.Infrastructure.Repositories;

namespace TrainRoutes.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Train Routes Finder - Results for Test Cases");

            // Load the graph
            var graphRepository = new GraphRepository();
            var graph = graphRepository.LoadGraph("Input.txt");

            // Initialize the route calculator
            IRouteCalculator routeCalculator = new RouteCalculator(graph);

            // Test #1: The distance of the route A=>B=>C is 9
            var calculateRouteDistance = new CalculateRouteDistance(routeCalculator);
            Console.WriteLine($"Test #1: {calculateRouteDistance.Execute(new[] { "A", "B", "C" })}");

            // Test #2: The distance of the route A=>D is 5
            Console.WriteLine($"Test #2: {calculateRouteDistance.Execute(new[] { "A", "D" })}");

            // Test #3: The distance of the route A=>D=>C is 13
            Console.WriteLine($"Test #3: {calculateRouteDistance.Execute(new[] { "A", "D", "C" })}");

            // Test #4: The distance of the route A=>E=>B=>C=>D is 22
            Console.WriteLine($"Test #4: {calculateRouteDistance.Execute(new[] { "A", "E", "B", "C", "D" })}");

            // Test #5: The distance of the route A=>E=>D
            var distance = routeCalculator.CalculateRouteDistance(new[] { "A", "E", "D" });
            if (distance == null)
            {
                Console.WriteLine("Test #5: doesn't exist");
            }
            else
            {
                Console.WriteLine($"Test #5: {distance}");
            }

            // Test #6: Number of trips from C to C with maximum 3 stops is 2
            var countTrips = new CountTrips(routeCalculator);
            Console.WriteLine($"Test #6: {countTrips.Execute("C", "C", 3)}");

            // Test #7: Number of trips from A to C with exactly 4 stops is 3
            Console.WriteLine($"Test #7: {countTrips.Execute("A", "C", 4, true)}");

            // Test #8: The length of the shortest route from A to C is 9
            var findShortestRoute = new FindShortestRoute(routeCalculator);
            Console.WriteLine($"Test #8: {findShortestRoute.Execute("A", "C")}");

            // Test #9: The length of the shortest route from B to B is 9
            Console.WriteLine($"Test #9: {findShortestRoute.Execute("B", "B")}");

            // Test #10: The number of trips from C to C with distance less than 30 is 7
            var countTripsByDistance = new CountTripsByDistance(routeCalculator);
            Console.WriteLine($"Test #10: {countTripsByDistance.Execute("C", "C", 30)}");
        }
    }
}
