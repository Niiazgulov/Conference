namespace Domain.Handlers.Contracts
{
    public interface ICheckSendedHandler
    {
        Task<string> CheckSended(Guid id);
    }
}
