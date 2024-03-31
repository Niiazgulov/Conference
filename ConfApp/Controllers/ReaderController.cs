using Domain.Handlers.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReaderController : ControllerBase
    {
        private IReaderHandler _readerHandler;

        public ReaderController(IReaderHandler readerHandler)
        {
            _readerHandler = readerHandler;
        }

        [HttpGet("applications/{id}")]
        public async Task<IActionResult> GetAppsById(Guid id)
        {
            try
            {
                var app = await _readerHandler.GetAppsById(id);
                if (app == null)
                    return NotFound();
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("applications/unsubmittedOlder=\"{datetime}\"")]
        public async Task<IActionResult> GetUnsubmittedApps([FromRoute] DateTime datetime)
        {
            try
            {
                var apps = await _readerHandler.GetUnsubmittedApps(datetime);
                return Ok(apps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("applications/submittedAfter=\"{datetime}\"")]
        public async Task<IActionResult> GetSubmittedApps([FromRoute] DateTime datetime)
        {
            try
            {
                var apps = await _readerHandler.GetSubmittedApps(datetime);
                return Ok(apps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("users/{authorId}/currentapplication")]
        public async Task<IActionResult> GetAppByAuthorId(Guid authorId)
        {
            try
            {
                var app = await _readerHandler.GetAppByAuthorId(authorId);
                if (app == null)
                    return NotFound();
                return Ok(app);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        /*
        [HttpGet("applications/submittedAfter=\"{datetime}\"")]
        public async Task<IActionResult> GetSubmittedApps([FromRoute] DateTime datetime)
        {
            try
            {
                var apps = await _getActivitiesHandler.GetSubmittedApps(datetime);
                return Ok(apps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        }*/




        [HttpGet("activities")]
        public IActionResult Get()
        {
            return Ok(_readerHandler.GetActivities());
        }


    }
}
