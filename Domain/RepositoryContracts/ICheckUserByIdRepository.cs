namespace Domain.Repository
{
    public interface ICheckUserByIdRepository
    {
        Task<bool> CheckUserById(Guid id);
    }
}

