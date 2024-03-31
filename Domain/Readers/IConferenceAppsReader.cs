namespace Domain.Readers
{
    public interface IConferenceAppsReader
    {
        //Domain.Activities[] GetActivities();
        //Domain.Applications AddApps(Domain.Applications apps);

        Task<Applications> GetAppsById(Guid id);
        Task<Applications> GetAppByAuthorId(Guid author);
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime);
        Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime);
        Applications GetCurrentApps(Guid author);
        

    }
}
 