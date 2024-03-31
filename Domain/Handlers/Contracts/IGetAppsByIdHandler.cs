namespace Domain.Handlers.Contracts
{
    public interface IGetAppsByIdHandler
    {
        Task<Applications?> GetAppsById(Guid id);
    }
}
