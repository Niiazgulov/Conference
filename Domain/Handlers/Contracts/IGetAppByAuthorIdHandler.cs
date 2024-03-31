namespace Domain.Handlers.Contracts
{
    public interface IGetAppByAuthorIdHandler
    {
        Task<Applications?> GetAppByAuthorId(Guid id);
    }
}
