namespace Domain.Readers
{
    public interface IConferenceAppsReader
    {
        //Domain.Activities[] GetActivities();
        //Domain.Applications AddApps(Domain.Applications apps);

        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime);
        Applications GetUnsubmittedOlderApps(string date);
        Applications GetCurrentApps(Guid author);
        Applications GetAppsById(Guid id);

    }
}
 