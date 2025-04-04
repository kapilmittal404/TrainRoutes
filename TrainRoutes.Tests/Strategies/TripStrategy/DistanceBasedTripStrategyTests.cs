using FluentAssertions;
using TrainRoutes.Enums;
using TrainRoutes.Strategies.TripStrategy;

namespace TrainRoutes.Tests.Strategies.TripStrategy;

public class DistanceBasedTripStrategyTests
{
    private Graph graph1;
    private Graph graph2;

    [SetUp]
    public void SetUp()
    {
        var route1 = new Route("A", "B", 5);
        var route2 = new Route("B", "C", 4);
        var route3 = new Route("C", "D", 8);
        var route4 = new Route("D", "C", 8);
        var route5 = new Route("D", "E", 6);
        var route6 = new Route("A", "D", 5);
        var route7 = new Route("C", "E", 2);
        var route8 = new Route("E", "B", 3);
        var route9 = new Route("A", "E", 7);
        graph1 = new Graph();
        graph1.AddRoute(route1);
        graph1.AddRoute(route2);
        graph1.AddRoute(route3);
        graph1.AddRoute(route4);
        graph1.AddRoute(route5);
        graph1.AddRoute(route6);
        graph1.AddRoute(route7);
        graph1.AddRoute(route8);
        graph1.AddRoute(route9);
        
        // Create a simple graph for testing
        graph2 = new Graph();

        // Add routes (edges) between towns
        graph2.AddRoute(new Route("A", "B", 5));
        graph2.AddRoute(new Route("B", "C", 4));
        graph2.AddRoute(new Route("A", "C", 10));
        graph2.AddRoute(new Route("C", "D", 3));
    }

    // ...existing code...
    [Test]
    public void TestCase10()
    {
        // Act: Get trips from C to C where distance is less than 30
        var tripCount = new DistanceBasedTripStrategy().GetTripCount(graph1, "C", "C", CountCondition.LessThan, 30);

        // Assert: The number of trips from A to C with distance less than 10 should be 7 ( C=>D=>C, C=>D=>C=>E=>B=>C, C=>D=>E=>B=>C, C=>E=>B=>C, C=>E=>B=>C=>D=>C, C=>E=>B=>C=>E=>B=>C, C=>E=>B=>C=>E=>B=>C=>E=>B=>C )
        tripCount.Should().Be(7);
    }

    [Test]
    public void GetTripCount_ShouldReturnCorrectCount_WhenTripsWithDistanceLessThanCondition()
    {
        // Act: Get trips from A to C where distance is less than 10
        var tripCount = new DistanceBasedTripStrategy().GetTripCount(graph2, "A", "C", CountCondition.LessThan, 10);

        // Assert: The number of trips from A to C with distance less than 10 should be 1 (A -> B -> C)
        tripCount.Should().Be(1);
    }

    [Test]
    public void GetTripCount_ShouldReturnZero_WhenNoTripsSatisfyDistanceCondition()
    {
        // Act: Get trips from A to D where distance is less than 5
        var tripCount = new DistanceBasedTripStrategy().GetTripCount(graph2, "A", "D", CountCondition.LessThan, 5);

        // Assert: There are no trips from A to D with a distance less than 5
        tripCount.Should().Be(0);
    }

    [Test]
    public void GetTripCount_ShouldReturnZero_WhenNoRoutesAvailable()
    {
        // Arrange: Create an empty graph
        var emptyGraph = new Graph();

        // Act: Get trips from A to C in an empty graph
        var tripCount = new DistanceBasedTripStrategy().GetTripCount(emptyGraph, "A", "C", CountCondition.LessThan, 10);

        // Assert: No trips should be possible in an empty graph
        tripCount.Should().Be(0);
    }

    [Test]
    public void GetTripCount_ShouldReturnCorrectCount_WhenExactRouteAndDistance()
    {
        // Act: Get trips from A to C where distance is less than 15
        var tripCount = new DistanceBasedTripStrategy().GetTripCount(graph2, "A", "C", CountCondition.LessThan, 15);

        // Assert: The number of trips from A to C with distance less than 15 should be 2 (A -> B -> C and A -> C directly)
        tripCount.Should().Be(2);
    }

    [TearDown]
    public void TearDown()
    {
        // Reset graph for next test
        graph1 = null;
        graph2 = null;
    }
}