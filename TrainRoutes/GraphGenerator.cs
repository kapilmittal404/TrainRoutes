namespace TrainRoutes;

public class GraphGenerator
{
    public static Graph GenerateGraph(string[] input)
    {
        var graph = new Graph();

        try
        {
            foreach (var inputItem in input)
            {
                var parts = inputItem.Split(',');
                var fromTown = parts[0].Trim();
                var toTown = parts[1].Trim();
                var distance = int.Parse(parts[2].Trim());

                var route = new Route(fromTown, toTown, distance);
                graph.AddRoute(route);
            }
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error occured while generating graph", e);
        }

        return graph;
    }
}