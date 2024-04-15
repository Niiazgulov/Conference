using Application.Handlers.Contracts.QueryHandlers;
using Domain.Models;
using Domain.ReadersContracts;

namespace Application.Handlers.QueryHandlers
{
    public class GetAppsByIdHandler : IGetAppsByIdHandler
    {
        private IApplicationReader _applicationReader;
        public GetAppsByIdHandler(IApplicationReader applicationReader)
        {
            _applicationReader = applicationReader;
        }

        public async Task<Applications?> GetAppsById(Guid id)
        {
            Applications? app = await _applicationReader.GetAppsById(id);

            return app;
        }
    }
}
