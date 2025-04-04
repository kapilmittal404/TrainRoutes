namespace TrainRoutes;

/// <summary>
/// This is our Route DTO class
/// </summary>
public class Route
{
    public string FromTown { get; }
    public string ToTown { get; }
    public int Distance { get; }

    public Route(string fromTown, string toTown, int distance)
    {
        FromTown = fromTown;
        ToTown = toTown;
        Distance = distance;
    }
}