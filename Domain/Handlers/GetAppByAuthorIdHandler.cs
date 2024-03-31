using Domain.Handlers.Contracts;
using Domain.Readers;

namespace Domain.Handlers
{
    public class GetAppByAuthorIdHandler : IGetAppByAuthorIdHandler
    {
        private IGetApplicationByAuthorIdReader _getApplicationByAuthorIdReader;
        public GetAppByAuthorIdHandler(IGetApplicationByAuthorIdReader getApplicationByAuthorIdReader)
        {
            _getApplicationByAuthorIdReader = getApplicationByAuthorIdReader;
        }

        public Task<Applications?> GetAppByAuthorId(Guid id)
        {
            Task<Applications?> app = _getApplicationByAuthorIdReader.GetAppByAuthorId(id);

            return app;
        }

    }
}
