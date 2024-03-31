namespace Domain.Handlers.Contracts
{
    public interface IAddNewApplicationHandler
    {
        Task<Applications> AddApps(NewAppDTO app);
    }
}
