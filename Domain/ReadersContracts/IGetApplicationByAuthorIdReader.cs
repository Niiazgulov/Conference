namespace Domain.Readers
{
    public interface IGetApplicationByAuthorIdReader
    {
        Task<Applications?> GetAppByAuthorId(Guid author);
    }
}
