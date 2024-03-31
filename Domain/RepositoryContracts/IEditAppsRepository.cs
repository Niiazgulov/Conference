namespace Domain.RepositoryContracts
{
    public interface IEditAppsRepository
    {
        Task<Applications?> EditApps(Guid id, EditedAppDTO app);
    }
}
