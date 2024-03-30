namespace Domain.Repository
{
    public interface IConferenceAppsRepository
    {
        //public Task<IEnumerable<Domain.Applications>> GetAppById(Guid id);

        Task<Applications> AddApps(NewAppDTO app);
        Task<Applications> EditApps(Guid id, EditedAppDTO app);
        Task DeleteApps(Guid id);
        void AddAppsToReview(Guid id);

    }
}

