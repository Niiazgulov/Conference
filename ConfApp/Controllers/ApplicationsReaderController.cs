using Domain.Handlers;
using Domain.Handlers.Contract;
using Domain.Handlers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsReaderController : ControllerBase
    {
        private IGetAppsByIdHandler _getAppsByIdHandler;
        private IGetUnsubmittedAppsHandler _getUnsubmittedAppsHandler;
        private IGetSubmittedAppsHandler _getSubmittedAppsHandler;
        private IGetAppByAuthorIdHandler _getAppByAuthorIdHandler;

        public ApplicationsReaderController(IGetAppsByIdHandler getAppsByIdHandler, IGetUnsubmittedAppsHandler getUnsubmittedAppsHandler, IGetSubmittedAppsHandler getSubmittedAppsHandler, IGetAppByAuthorIdHandler getAppByAuthorIdHandler)
        {
            _getAppsByIdHandler = getAppsByIdHandler;
            _getUnsubmittedAppsHandler = getUnsubmittedAppsHandler;
            _getSubmittedAppsHandler = getSubmittedAppsHandler;
            _getAppByAuthorIdHandler = getAppByAuthorIdHandler;
        }

        [HttpGet("applications/{id}")]
        public async Task<IActionResult> GetAppsById(Guid id)
        {
            try
            {
                var app = await _getAppsByIdHandler.GetAppsById(id);
                if (app == null)
                    return NotFound();
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
                    return NotFound();
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("applications/unsubmittedOlder=\"{datetime}\"")]
        public async Task<IActionResult> GetUnsubmittedApps([FromRoute] DateTime datetime)
        {
            try
            {
                var apps = await _getUnsubmittedAppsHandler.GetUnsubmittedApps(datetime);
                return Ok(apps);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("applications/submittedAfter=\"{datetime}\"")]
        public async Task<IActionResult> GetSubmittedApps([FromRoute] DateTime datetime)
        {
            try
            {
                var apps = await _getSubmittedAppsHandler.GetSubmittedApps(datetime);
                return Ok(apps);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
