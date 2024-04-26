using Application.Handlers.Contracts.QueryHandlers;
using Domain.Models;
using Domain.ReadersContracts;

namespace Application.Handlers.QueryHandlers
{
    public class GetAppByAuthorIdHandler : IGetAppByAuthorIdHandler
    {
        private IApplicationReader _applicationReader;
        public GetAppByAuthorIdHandler(IApplicationReader applicationReader)
        {
            _applicationReader = applicationReader;
        }

        public async Task<Applications?> GetAppByAuthorId(Guid id)
        {
            var app = await _applicationReader.GetAppByAuthorId(id);

            return app;
        }
    }
}
