namespace Domain.Handlers.Contracts
{
    public interface IAddNewApplicationHandler
    {
        Task<(bool, string, Applications)> AddApps(NewAppDTO app);
    }
}
