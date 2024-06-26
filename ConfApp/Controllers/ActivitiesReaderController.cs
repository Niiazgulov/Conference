﻿using Application.Handlers.Contracts.QueryHandlers;
using Microsoft.AspNetCore.Mvc;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesReaderController : ControllerBase
    {
        private IGetActivitiesRequestHandler _getActivityHandler;
        public ActivitiesReaderController(IGetActivitiesRequestHandler getActivityHandler)
        {
            _getActivityHandler = getActivityHandler;
        }

        [HttpGet("activities")]
        public IActionResult Get()
        {
            return Ok(_getActivityHandler.GetActivities());
        }
    }
}