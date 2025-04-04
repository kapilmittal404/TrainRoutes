namespace TrainRoutes.Tests;
using FluentAssertions;

public class FileReaderTests
{
    private string testFilePath;

    [SetUp]
    public void SetUp()
    {
        // Initialize a temporary file path
        testFilePath = Path.Combine(Path.GetTempPath(), "testGraph.txt");
    }

    [Test]
    public void ReadGraphFromFile_ShouldReturnFileContent_WhenFileExists()
    {
        // Arrange: Create a file with known content
        var content = new string[]
        {
            "A,B,5",
            "B,C,3",
            "C,A,7"
        };
        File.WriteAllLines(testFilePath, content);

        // Act: Call the method
        var result = GraphFileReader.ReadGraphFromFile(testFilePath);

        // Assert: Verify the result
        result.Should().BeEquivalentTo(content);
    }

    [Test]
    public void ReadGraphFromFile_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
    {
        // Arrange: Set a path for a non-existing file
        var nonExistingFilePath = Path.Combine(Path.GetTempPath(), "nonExistingGraph.txt");

        // Act & Assert: Verify that a FileNotFoundException is thrown
        Action act = () => GraphFileReader.ReadGraphFromFile(nonExistingFilePath);
        act.Should().Throw<FileNotFoundException>()
            .WithMessage($"File with path {nonExistingFilePath} not found!");
    }

    [Test]
    public void ReadGraphFromFile_ShouldReturnEmptyArray_WhenFileIsEmpty()
    {
        // Arrange: Create an empty file
        File.WriteAllText(testFilePath, string.Empty);

        // Act: Call the method
        var result = GraphFileReader.ReadGraphFromFile(testFilePath);

        // Assert: Verify the result is an empty array
        result.Should().BeEmpty();
    }

    [Test]
    public void ReadGraphFromFile_ShouldReturnCorrectContent_WhenFileHasMultipleLines()
    {
        // Arrange: Create a file with known content
        var content = new string[]
        {
            "A,B,5",
            "B,C,3",
            "C,A,7",
            "D,E,2"
        };
        File.WriteAllLines(testFilePath, content);

        // Act: Call the method
        var result = GraphFileReader.ReadGraphFromFile(testFilePath);

        // Assert: Verify the result
        result.Should().HaveCount(4);
        result.Should().ContainEquivalentOf("A,B,5");
        result.Should().ContainEquivalentOf("D,E,2");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the file after the test
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}

public static class GraphFileReader
{
    public static string[] ReadGraphFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath);
        }
        throw new FileNotFoundException($"File with path {filePath} not found!");
    }
}