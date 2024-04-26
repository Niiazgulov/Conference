using Application.Handlers.Contracts.QueryHandlers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsReaderController : ControllerBase
    {
        private IGetAppsByIdHandler _getAppsByIdHandler;
        private IGetSubOrUnsubmittedAppsHandler _getSubOrUnsubmittedAppsHandler;
        private IGetAppByAuthorIdHandler _getAppByAuthorIdHandler;

        public ApplicationsReaderController(IGetAppsByIdHandler getAppsByIdHandler, IGetSubOrUnsubmittedAppsHandler getSubOrUnsubmittedAppsHandler, IGetAppByAuthorIdHandler getAppByAuthorIdHandler)
        {
            _getAppsByIdHandler = getAppsByIdHandler;
            _getSubOrUnsubmittedAppsHandler = getSubOrUnsubmittedAppsHandler;
            _getAppByAuthorIdHandler = getAppByAuthorIdHandler;
        }

        [HttpGet("applications/{id}")]
        public async Task<IActionResult> GetAppsById(Guid id)
        {
            try
            {
                var app = await _getAppsByIdHandler.GetAppsById(id);
                if (app == null)
                    return StatusCode(404, "ОШИБКА! Такой заявки не существует.");
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("users/{authorId}/currentapplication")]
        public async Task<IActionResult> GetAppByAuthorId(Guid authorId)
        {
            try
            {
                var app = await _getAppByAuthorIdHandler.GetAppByAuthorId(authorId);
                if (app == null)
                    return StatusCode(404, "ОШИБКА! Такого автора не существует.");
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetUnsubmittedApps(SubOrUnsubDTO req)
        {
            try
            {
                (bool res, string? message, IEnumerable<Applications>? newapps) = await _getSubOrUnsubmittedAppsHandler.GetSubOrUnSubmittedApps(req);
                if (res)
                {
                    return Ok(newapps);
                }
                return StatusCode(400, message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
