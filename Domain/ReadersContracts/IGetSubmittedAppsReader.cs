namespace Domain.Readers
{
    public interface IGetSubmittedAppsReader
    {
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime);
    }
}
