namespace Domain.Handlers.Contracts
{
    public interface IEditAppsHandler
    {
        Task<Applications?> EditApps(Guid id, EditedAppDTO app);
    }
}
