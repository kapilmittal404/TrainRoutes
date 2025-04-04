using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // inputFilePath should be updated where file is present.
        string inputFilePath = "C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt";
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Input file not found.");
            return;
        }

        Graph graph = new Graph();
        graph.LoadFromFile(inputFilePath);

        // Run tests
        Console.WriteLine("Test #1: " + graph.CalculateRouteDistance("A-B-C"));
        Console.WriteLine("Test #2: " + graph.CalculateRouteDistance("A-D"));
        Console.WriteLine("Test #3: " + graph.CalculateRouteDistance("A-D-C"));
        Console.WriteLine("Test #4: " + graph.CalculateRouteDistance("A-E-B-C-D"));
        Console.WriteLine("Test #5: " + graph.CalculateRouteDistance("A-E-D"));
        Console.WriteLine("Test #6: " + graph.CountTripsWithMaxStops('C', 'C', 3));
        Console.WriteLine("Test #7: " + graph.CountTripsWithExactStops('A', 'C', 4));
        Console.WriteLine("Test #8: " + graph.FindShortestRoute('A', 'C'));
        Console.WriteLine("Test #9: " + graph.FindShortestRoute('B', 'B'));
        Console.WriteLine("Test #10: " + graph.CountTripsWithMaxDistance('C', 'C', 30));
    }
}
