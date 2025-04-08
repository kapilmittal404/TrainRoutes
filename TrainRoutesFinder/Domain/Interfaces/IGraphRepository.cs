using TrainRoutes.Domain.Entities;

namespace TrainRoutes.Domain.Interfaces
{
    public interface IGraphRepository
    {
        TownGraph LoadGraph(string filePath);
    }
}
