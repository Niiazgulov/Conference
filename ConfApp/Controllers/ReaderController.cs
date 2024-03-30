using Domain.Handlers.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReaderController : ControllerBase
    {
        private IReaderHandler _getActivitiesHandler;

        public ReaderController(IReaderHandler getActivitiesHandler)
        {
            _getActivitiesHandler = getActivitiesHandler;
        }


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
        }








        [HttpGet("activities")]
        public IActionResult Get()
        {
            return Ok(_getActivitiesHandler.GetActivities());
        }


    }
}
