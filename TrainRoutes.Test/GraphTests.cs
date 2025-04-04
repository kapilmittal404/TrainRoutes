using System;
using Xunit;

public class GraphTests
{
    [Fact]
    public void TestCalculateRouteDistance()
    {
        Graph graph = new Graph();
        // inputFilePath should be updated where file is present.
        graph.LoadFromFile("C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt");

        Assert.Equal(9, graph.CalculateRouteDistance("A-B-C"));
        Assert.Equal(5, graph.CalculateRouteDistance("A-D"));
        Assert.Equal(13, graph.CalculateRouteDistance("A-D-C"));
        Assert.Equal(22, graph.CalculateRouteDistance("A-E-B-C-D"));
        Assert.Equal(-1, graph.CalculateRouteDistance("A-E-D"));
    }

    [Fact]
    public void TestCountTripsWithMaxStops()
    {
        Graph graph = new Graph();
        graph.LoadFromFile("C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt");

        Assert.Equal(2, graph.CountTripsWithMaxStops('C', 'C', 3));
    }

    [Fact]
    public void TestCountTripsWithExactStops()
    {
        Graph graph = new Graph();
        graph.LoadFromFile("C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt");

        Assert.Equal(3, graph.CountTripsWithExactStops('A', 'C', 4));
    }

    [Fact]
    public void TestFindShortestRoute()
    {
        Graph graph = new Graph();
        graph.LoadFromFile("C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt");

        Assert.Equal(9, graph.FindShortestRoute('A', 'C'));
        Assert.Equal(9, graph.FindShortestRoute('B', 'B'));
    }

    [Fact]
    public void TestCountTripsWithMaxDistance()
    {
        Graph graph = new Graph();
        graph.LoadFromFile("C:\\Users\\Admin\\source\\repos\\TrainRoutes\\Input.txt");

        Assert.Equal(7, graph.CountTripsWithMaxDistance('C', 'C', 30));
    }
}
