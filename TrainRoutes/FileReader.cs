namespace TrainRoutes;

public class FileReader
{
    /// <summary>
    /// The file reader function for our specific file format
    /// </summary>
    /// <param name="filePath">Input file path location in the project</param>
    /// <returns>The graph structure as provided by the input file</returns>
    public static string[] ReadGraphFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath);
        }
        throw new FileNotFoundException($"File with path {filePath} not found!");
    }
}
