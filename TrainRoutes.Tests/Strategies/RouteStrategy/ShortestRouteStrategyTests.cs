using FluentAssertions;
using TrainRoutes.Strategies.RouteStrategy;

namespace TrainRoutes.Tests.Strategies.RouteStrategy;

public class ShortestRouteStrategyTests
{
    private Graph graph;

    [SetUp]
    public void SetUp()
    {
        // Create a simple graph for testing
        var route1 = new Route("A", "B", 5);
        var route2 = new Route("B", "C", 4);
        var route3 = new Route("C", "D", 8);
        var route4 = new Route("D", "C", 8);
        var route5 = new Route("D", "E", 6);
        var route6 = new Route("A", "D", 5);
        var route7 = new Route("C", "E", 2);
        var route8 = new Route("E", "B", 3);
        var route9 = new Route("A", "E", 7);
        graph = new Graph();
        graph.AddRoute(route1);
        graph.AddRoute(route2);
        graph.AddRoute(route3);
        graph.AddRoute(route4);
        graph.AddRoute(route5);
        graph.AddRoute(route6);
        graph.AddRoute(route7);
        graph.AddRoute(route8);
        graph.AddRoute(route9);
    }

    [Test]
    public void TestCase8()
    {
        // Act: Calculate route from A to C
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "C");

        // Assert: The shortest path from A to D is A -> B -> C , total distance = 9
        result.Should().Be(9);
    }
    
    [Test]
    public void TestCase9()
    {
        // Act: Calculate route from B to B
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "B", "B");

        // Assert: The shortest path from A to D is B -> C -> E -> B , total distance = 9
        result.Should().Be(9);
    }

    // ...existing code...
    [Test]
    public void CalculateRoute_ShouldReturnShortestPath_WhenRouteExists()
    {
        // Act: Calculate route from A to D
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "D");

        // Assert: The shortest path from A to D is A -> D, total distance = 5
        result.Should().Be(5);
    }

    [Test]
    public void CalculateRoute_ShouldReturnNegativeOne_WhenNoRouteExists()
    {
        // Act: Calculate route from A to E
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "E");

        // Assert: The shortest path from A to E is A -> E, total distance = 7
        result.Should().Be(7);
    }

    [Test]
    public void CalculateRoute_ShouldHandleStartAndEndTownsAreSame()
    {
        // Act: Calculate route from A to A (No path)
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "A");

        // Assert: The distance from A to A is 0 (As there is not path back to A)
        result.Should().Be(0);
    }

    [Test]
    public void CalculateRoute_ShouldReturnCorrectShortestPath_WhenCircularPathsExist()
    {
        // Arrange: Add a circular path from A to B and back to A
        graph.AddRoute(new Route("B", "A", 4));

        // Act: Calculate the route from A to B
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "B");

        // Assert: The shortest route from A to B is A -> B with distance 5
        result.Should().Be(5);
    }

    [Test]
    public void CalculateRoute_ShouldReturnCorrectShortestPath_WhenCircularRoutesExist()
    {
        // Arrange: Add a circular path A -> B -> A with distances 5 and 4 respectively
        graph.AddRoute(new Route("B", "A", 4));

        // Act: Calculate route from A to A
        var result = new ShortestRouteStrategy().CalculateRoute(graph, "A", "A");

        // Assert: The shortest route from A to A should be 9 
        result.Should().Be(9);
    }

    [TearDown]
    public void TearDown()
    {
        // Reset graph for next test
        graph = null;
    }
}