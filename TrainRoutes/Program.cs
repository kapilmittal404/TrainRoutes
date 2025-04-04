using TrainRoutes;
using TrainRoutes.Enums;
using TrainRoutes.Strategies.RouteStrategy;
using TrainRoutes.Strategies.TripStrategy;

class Program
{
    static void Main(string[] args)
    {
        var inputData = FileReader.ReadGraphFromFile("Inputs\\Input.txt");
        var graph = GraphGenerator.GenerateGraph(inputData);

        var routeCalculator = new RouteCalculator(new DirectRouteStrategy());
        var distance1 = routeCalculator.GetRouteDistance(graph, "A", "B");
        var distance2 = routeCalculator.GetRouteDistance(graph, "A", "D");
        var distance3 = routeCalculator.GetRouteDistance(graph, "D", "C");
        var distance4 = routeCalculator.GetRouteDistance(graph, "A", "E");
        var distance5 = routeCalculator.GetRouteDistance(graph, "E", "B");
        var distance6 = routeCalculator.GetRouteDistance(graph, "B", "C");
        var distance7 = routeCalculator.GetRouteDistance(graph, "C", "D");


        Console.WriteLine($"Scenario 1: Distance for A=>B=>C: {distance1 + distance6}");
        Console.WriteLine($"Scenario 2: Distance for A=>D: {distance2}");
        Console.WriteLine($"Scenario 3: Distance for A=>D=>C: {distance2 + distance3}");
        Console.WriteLine($"Scenario 4: Distance for A=>E=>B=>C=>D: {distance4 + distance5 + distance6 + distance7}");

        try
        {
            var distance8 = routeCalculator.GetRouteDistance(graph, "E", "D");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Scenario 5: Distance for A=>E=>D: " + e.Message);
        }

        var tripCalculator = new TripCalculator(new StopBasedTripStrategy());
        var tripCount1 = tripCalculator.GetTripCount(graph, "C", "C", 3, CountCondition.LessThanEqualTo);
        var tripCount2 = tripCalculator.GetTripCount(graph, "A", "C", 4);
        Console.WriteLine($"Scenario 6: Trip count from C=>C with at maximum 3 stops: {tripCount1}");
        Console.WriteLine($"Scenario 7: Trip count from A=>C with exactly 4 stops: {tripCount2}");

        routeCalculator = new RouteCalculator(new ShortestRouteStrategy());

        var distance9 = routeCalculator.GetRouteDistance(graph, "A", "C");
        var distance10 = routeCalculator.GetRouteDistance(graph, "B", "B");

        Console.WriteLine($"Scenario 8: Distance for A=>C: {distance9}");
        Console.WriteLine($"Scenario 9: Distance for B=>B: {distance10}");



        tripCalculator = new TripCalculator(new DistanceBasedTripStrategy());
        var tripCount3 = tripCalculator.GetTripCount(graph, "C", "C", 30, CountCondition.LessThan);
        Console.WriteLine($"Scenario 10: Trip count from C=>C with distance less than 30: {tripCount3}");
    }
}