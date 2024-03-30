namespace Domain.Handlers.Contract
{
    public interface IReaderHandler
    {
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime date);
        Activities[] GetActivities();

    }
}
