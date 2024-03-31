namespace Domain.Readers
{
    public interface IGetApplicationByIdReader
    {
        Task<Applications?> GetAppsById(Guid id);
    }
}
