using Domain.Handlers.Contracts;
using Domain.Readers;

namespace Domain.Handlers
{
    public class GetAppsByIdHandler : IGetAppsByIdHandler
    {
        private IGetApplicationByIdReader _getApplicationByIdReader;
        public GetAppsByIdHandler(IGetApplicationByIdReader getApplicationByIdReader)
        {
            _getApplicationByIdReader = getApplicationByIdReader;
        }

        public Task<Applications?> GetAppsById(Guid id)
        {
            Task<Applications?> app = _getApplicationByIdReader.GetAppsById(id);

            return app;
        }
    }
}
