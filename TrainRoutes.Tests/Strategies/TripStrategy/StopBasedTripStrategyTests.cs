using FluentAssertions;
using TrainRoutes.Enums;
using TrainRoutes.Strategies.TripStrategy;

namespace TrainRoutes.Tests.Strategies.TripStrategy;

public class StopBasedTripStrategyTests
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
        graph2.AddRoute(new Route("A", "D", 8));
    }

    [Test]
    public void TestCase6()
    {
        // Act: Get trips from C to C with less than or equal to 3 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph1, "C", "C", CountCondition.LessThanEqualTo, 3);

        // Assert: The number of trips from C to C with stops less than or equal to 3 should be 2
        //  ( C=>D=>C, C=>E=>B=>C )
        tripCount.Should().Be(2);
    }
    
    [Test]
    public void TestCase7()
    {
        // Act: Get trips from A to C with exactly 4 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph1, "A", "C", CountCondition.EqualTo, 4);

        // Assert: The number of trips from A to C with stops exactly 4 is 3
        // ( A=>B=>C=>D=>C, A=>D=>C=>D=>C, A=>D=>E=>B=>C )
        tripCount.Should().Be(3);
    }
    
    [Test]
    public void GetTripCount_ShouldReturnCorrectCount_WhenTripsWithLessThanEqualToStops()
    {
        // Act: Get trips from A to D with less than or equal to 2 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "D", CountCondition.LessThanEqualTo, 2);

        // Assert: The number of trips from A to D with stops less than or equal to 2 should be 2
        // (A -> B -> C -> D, A -> D)
        tripCount.Should().Be(2);
    }

    // Test case for trips with exactly a certain number of stops
    [Test]
    public void GetTripCount_ShouldReturnCorrectCount_WhenTripsWithExactStops()
    {
        // Act: Get trips from A to D with exactly 2 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "D", CountCondition.EqualTo, 2);

        // Assert: The number of trips from A to D with exactly 2 stops should be 1 (A -> D)
        tripCount.Should().Be(1);
    }

    // Test case for no trips matching the condition
    [Test]
    public void GetTripCount_ShouldReturnZero_WithOneStop()
    {
        // Act: Get trips from A to D with exactly 1 stop
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "D", CountCondition.EqualTo, 1);

        // Assert: There are no trips from A to D with exactly 1 stop
        tripCount.Should().Be(1);
    }
    
    // Test case for no trips matching the condition
    [Test]
    public void GetTripCount_ShouldReturnZero_WhenNoTripsSatisfyCondition()
    {
        // Act: Get trips from A to D with exactly 1 stop
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "D", CountCondition.EqualTo, 5);

        // Assert: There are no trips from A to D with exactly 1 stop
        tripCount.Should().Be(0);
    }

    // Test case for empty graph
    [Test]
    public void GetTripCount_ShouldReturnZero_WhenGraphIsEmpty()
    {
        // Arrange: Create an empty graph
        var emptyGraph = new Graph();

        // Act: Get trips from A to D in an empty graph
        var tripCount = new StopBasedTripStrategy().GetTripCount(emptyGraph, "A", "D", CountCondition.LessThanEqualTo, 2);

        // Assert: No trips should be possible in an empty graph
        tripCount.Should().Be(0);
    }

    // Test case for trips with multiple towns
    [Test]
    public void GetTripCount_ShouldReturnCorrectCount_WhenTripsWithMultipleTownsAndStops()
    {
        // Act: Get trips from A to C with less than or equal to 3 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "C", CountCondition.LessThanEqualTo, 3);

        // Assert: The number of trips from A to C with stops less than or equal to 3 should be 3
        // (A -> C, A -> B -> C)
        tripCount.Should().Be(2);
    }

    // Test case for trips with zero stops (same town)
    [Test]
    public void GetTripCount_ShouldReturnZero_WhenTripsWithZeroStops()
    {
        // Act: Get trips from A to B with exactly 0 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "A", "B", CountCondition.EqualTo, 0);

        // Assert: There are no trips from A to B with exactly 0 stops
        tripCount.Should().Be(0);
    }
    
    // Test case for trips with zero stops (same town)
    [Test]
    public void GetTripCount_ShouldReturnZero_WhenTripsDoesntExist()
    {
        // Act: Get trips from D to A with exactly 0 stops
        var tripCount = new StopBasedTripStrategy().GetTripCount(graph2, "D", "A", CountCondition.EqualTo, 1);

        // Assert: There are no trips from D to A with exactly 0 stops
        tripCount.Should().Be(0);
    }

    [TearDown]
    public void TearDown()
    {
        // Reset graph for next test
        graph2 = null;
    }
}