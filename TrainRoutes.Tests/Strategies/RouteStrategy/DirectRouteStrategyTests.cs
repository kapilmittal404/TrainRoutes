using FluentAssertions;
using TrainRoutes.Strategies.RouteStrategy;

namespace TrainRoutes.Tests.Strategies.RouteStrategy;

public class DirectRouteStrategyTests
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
    public void TestCase1()
    {
        // Act: Calculate route from A to B (direct route) + B to C
        var result1 = new DirectRouteStrategy().CalculateRoute(graph, "A", "B");
        var result2 = new DirectRouteStrategy().CalculateRoute(graph, "B", "C");

        // Assert: The distance from A to B + B to C is 9
        var result = result1 + result2;
        result.Should().Be(9);
    }
    
    [Test]
    public void TestCase2()
    {
        // Act: Calculate route from A to D (direct route)
        var result = new DirectRouteStrategy().CalculateRoute(graph, "A", "D");

        // Assert: The distance from A to D is 5
        result.Should().Be(5);
    }
    
    [Test]
    public void TestCase3()
    {
        // Act: Calculate route from A to D (direct route) + D to C
        var result1 = new DirectRouteStrategy().CalculateRoute(graph, "A", "D");
        var result2 = new DirectRouteStrategy().CalculateRoute(graph, "D", "C");

        // Assert: The distance from A to D + D to C is 13
        var result = result1 + result2;
        result.Should().Be(13);
    }
    
    [Test]
    public void TestCase4()
    {
        // Act: Calculate route from A to E (direct route) + E to B + B to C + C to D
        var result1 = new DirectRouteStrategy().CalculateRoute(graph, "A", "E");
        var result2 = new DirectRouteStrategy().CalculateRoute(graph, "E", "B");
        var result3 = new DirectRouteStrategy().CalculateRoute(graph, "B", "C");
        var result4 = new DirectRouteStrategy().CalculateRoute(graph, "C", "D");

        // Assert: The distance from A to E + E to B + B to C + C to D is 22
        var result = result1 + result2 + result3 + result4;
        result.Should().Be(22);
    }
    
    [Test]
    public void TestCase5()
    {
        // Act & Assert: Attempt to calculate route from a non-existent town
        Action act = () => new DirectRouteStrategy().CalculateRoute(graph, "E", "D");

        // Assert: An ArgumentException is thrown with the correct message
        act.Should().Throw<ArgumentException>()
            .WithMessage("There is no direct route from E to D");
    }
    
    [Test]
    public void CalculateRoute_ShouldReturnDistance_WhenRouteExists()
    {
        // Act: Calculate route from A to B (direct route)
        var result = new DirectRouteStrategy().CalculateRoute(graph, "A", "B");

        // Assert: The distance from A to B is 5
        result.Should().Be(5);
    }

    [Test]
    public void CalculateRoute_ShouldReturnDistance_WhenRouteExistsFromBtoC()
    {
        // Act: Calculate route from B to C (direct route)
        var result = new DirectRouteStrategy().CalculateRoute(graph, "B", "C");

        // Assert: The distance from B to C is 4
        result.Should().Be(4);
    }

    [Test]
    public void CalculateRoute_ShouldThrowArgumentException_WhenNoRouteExists()
    {
        // Act & Assert: Attempt to calculate route from A to C (no direct route)
        Action act = () => new DirectRouteStrategy().CalculateRoute(graph, "A", "C");

        // Assert: An ArgumentException is thrown with the correct message
        act.Should().Throw<ArgumentException>()
            .WithMessage("There is no direct route from A to C");
    }

    [Test]
    public void CalculateRoute_ShouldThrowArgumentException_WhenEndTownDoesNotExist()
    {
        // Act & Assert: Attempt to calculate route to a non-existent town
        Action act = () => new DirectRouteStrategy().CalculateRoute(graph, "A", "Z");

        // Assert: An ArgumentException is thrown with the correct message
        act.Should().Throw<ArgumentException>()
            .WithMessage("There is no direct route from A to Z");
    }

    [TearDown]
    public void TearDown()
    {
        // Reset graph for next test
        graph = null;
    }
}