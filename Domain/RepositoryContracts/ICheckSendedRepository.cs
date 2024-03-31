namespace Domain.RepositoryContracts
{
    public interface ICheckSendedRepository
    {
        Task<string> CheckSended(Guid id);
    }
}
