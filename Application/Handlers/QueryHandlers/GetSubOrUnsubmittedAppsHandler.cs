using Application.Handlers.Contracts.QueryHandlers;
using Application.Validators.Contracts;
using Domain.Models;
using Domain.ReadersContracts;

namespace Application.Handlers.QueryHandlers
{
    public class GetSubOrUnsubmittedAppsHandler : IGetSubOrUnsubmittedAppsHandler
    {
        private IApplicationReader _applicationReader;
        private IGetSubOrUnsubValidator _appsValidator;
        public GetSubOrUnsubmittedAppsHandler(IApplicationReader applicationReader, IGetSubOrUnsubValidator appsValidator)
        {
            _applicationReader = applicationReader;
            _appsValidator = appsValidator;
        }
        public async Task<(bool, string?, IEnumerable<Applications>?)> GetSubOrUnSubmittedApps(SubOrUnsubDTO req)
        {

            (bool res, bool sended, DateTime? datetime, string? message) = _appsValidator.Validate(req);
            if (res != true)
            {
                return (false, message, null);
            }

            if (sended == true)
            {
                var submittedApp = await _applicationReader.GetSubmittedApps(datetime, sended);
                return (true, String.Empty, submittedApp);
            }
            var unsubmittedApp = await _applicationReader.GetUnsubmittedApps(datetime, sended);
            return (true, String.Empty, unsubmittedApp);
        }
    }
}
