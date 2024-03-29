using Domain.QueryHandlers.GetActivities.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfController : ControllerBase
    {
        private IGetActivitiesHandler _getActivitiesHandler;

        public ConfController(IGetActivitiesHandler getActivitiesHandler)
        {
            _getActivitiesHandler = getActivitiesHandler;
        }

        [HttpGet("activities")]
        public IActionResult Get()
        {
            return Ok(_getActivitiesHandler.GetActivities());
        }
    }
}
