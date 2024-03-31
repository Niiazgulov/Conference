namespace Domain.Readers
{
    public interface IGetUnsubmittedAppsReader
    {
        Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime);
    }
}
