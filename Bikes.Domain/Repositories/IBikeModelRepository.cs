using Bikes.Domain.Models;

namespace Bikes.Domain.Repositories;

/// <summary>
/// Repository for bike model data access
/// </summary>
public interface IBikeModelRepository
{
    public List<BikeModel> GetAllModels();
    public BikeModel? GetModelById(int id);
    public void AddModel(BikeModel model);
    public void UpdateModel(BikeModel model);
    public void DeleteModel(int id);
}