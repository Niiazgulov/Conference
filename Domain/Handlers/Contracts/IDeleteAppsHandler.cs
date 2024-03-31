namespace Domain.Handlers.Contracts
{
    public interface IDeleteAppsHandler
    {
        Task DeleteApps(Guid id);
    }
}
