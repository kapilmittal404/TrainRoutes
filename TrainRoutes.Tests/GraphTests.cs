using FluentAssertions;

namespace TrainRoutes.Tests;

public class GraphTests
{
    // ...existing code...
    [Test]
    public void AddRoute_Success_AddSingleRoute()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);

        //Act
        var graph = new Graph();
        graph.AddRoute(route1);

        //Assert
        var output1 = graph.GetRoutesFromTown("A");
        output1.Count.Should().Be(1);
        output1[0].FromTown.Should().Be("A");
        output1[0].ToTown.Should().Be("B");
        output1[0].Distance.Should().Be(1);
    }
    
    [Test]
    public void AddRoute_Success_AddMultipleUniqueStops()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);
        var route2 = new Route("D", "C", 2);

        //Act
        var graph = new Graph();
        graph.AddRoute(route1);
        graph.AddRoute(route2);

        //Assert
        var output1 = graph.GetRoutesFromTown("A");
        output1.Count.Should().Be(1);
        output1[0].FromTown.Should().Be("A");
        output1[0].ToTown.Should().Be("B");
        output1[0].Distance.Should().Be(1);

        var output2 = graph.GetRoutesFromTown("D");
        output2.Count.Should().Be(1);
        output2[0].FromTown.Should().Be("D");
        output2[0].ToTown.Should().Be("C");
        output2[0].Distance.Should().Be(2);
    }
    
    [Test]
    public void AddRoute_Success_AddSameFromStop()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);
        var route2 = new Route("A", "C", 2);

        //Act
        var graph = new Graph();
        graph.AddRoute(route1);
        graph.AddRoute(route2);

        //Assert
        var output = graph.GetRoutesFromTown("A");
        output.Count.Should().Be(2);
        output[0].FromTown.Should().Be("A");
        output[0].ToTown.Should().Be("B");
        output[0].Distance.Should().Be(1);
        output[1].FromTown.Should().Be("A");
        output[1].ToTown.Should().Be("C");
        output[1].Distance.Should().Be(2);
    }

    [Test]
    public void GetRoutesFromTown_Success()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);
        var graph = new Graph();
        graph.AddRoute(route1);

        //Act
        var output = graph.GetRoutesFromTown("A");

        //Assert
        output.Count.Should().Be(1);
        output[0].FromTown.Should().Be("A");
        output[0].ToTown.Should().Be("B");
        output[0].Distance.Should().Be(1);
    }

    [Test]
    public void GetRoutesFromTown_NoFromRoute()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);
        var graph = new Graph();
        graph.AddRoute(route1);
        
        //Act
        var output = graph.GetRoutesFromTown("B");
        
        //Assert
        output.Count.Should().Be(0);
    }

    [Test]
    public void GetAllTowns_Success()
    {
        //Arrange
        var route1 = new Route("A", "B", 1);
        var route2 = new Route("A", "C", 2);
        var route3 = new Route("D", "C", 3);
        var route4 = new Route("D", "E", 4);
        var graph = new Graph();
        graph.AddRoute(route1);
        graph.AddRoute(route2);
        graph.AddRoute(route3);
        graph.AddRoute(route4);
        
        //Act
        var output = graph.GetAllTowns();
        
        //Assert
        output.Count().Should().Be(5);
    }
}