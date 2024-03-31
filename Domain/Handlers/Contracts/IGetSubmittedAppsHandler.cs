namespace Domain.Handlers.Contract
{
    public interface IGetSubmittedAppsHandler
    {
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime date);

    }
}
