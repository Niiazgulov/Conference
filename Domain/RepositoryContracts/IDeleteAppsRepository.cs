namespace Domain.RepositoryContracts
{
    public interface IDeleteAppsRepository
    {
        Task DeleteApps(Guid id);
    }
}
