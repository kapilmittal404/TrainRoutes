using FluentAssertions;

namespace TrainRoutes.Tests;

public class GraphGeneratorTests
{
    // ...existing code...
    [Test]
    public void GenerateGraph_Success()
    {
        //Arrange
        var input = new string[] { "A,B,5", "B,C,4", "C,D,8", "D,C,8", "D,E,6", "A,D,5", "C,E,2", "E,B,3", "A,E,7" };

        //Act
        var output = GraphGenerator.GenerateGraph(input);

        //Assert
        output.GetAllTowns().Count().Should().Be(5);
    }
    
    [Test]
    public void GenerateGraph_ArgumentException()
    {
        //Arrange
        var input = new string[] { "A5" };

        //Act and Assert
        Assert.Throws<ArgumentException>(() => GraphGenerator.GenerateGraph(input));
    }
}