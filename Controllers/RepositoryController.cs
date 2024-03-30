﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Handlers.Contract;
using Domain.Handlers.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoryController : ControllerBase
    {
        private IRepositoryHandler _repositoryHandler;

        public RepositoryController(IRepositoryHandler repositoryHandler)
        {
            _repositoryHandler = repositoryHandler;
        }

        [HttpPost("applications")]
        public async Task<IActionResult> CreateApp(NewAppDTO app)
        {
            try
            {
                if (app.Author.ToString() == "00000000-0000-0000-0000-000000000000")
                    return StatusCode(400, "Укажите идентификатор автора в формате Guid!");

                var newapp = await _repositoryHandler.AddApps(app);
                if (newapp == null)
                    return NotFound();

                return Ok(newapp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApp(Guid id, EditedAppDTO app)
        {
            try
            {
                //var dbApp = await _repositoryHandler.GetAppById(id);
                //if (dbApp == null)
                //    return NotFound();
                var editedapp = await _repositoryHandler.EditApps(id, app);
                if (editedapp == null)
                    return NotFound();

                return Ok(editedapp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(Guid id)
        {
            try
            {
                //var dbCompany = await _companyRepo.GetCompany(id);
               // if (dbCompany == null)
                //    return NotFound();
                await _repositoryHandler.DeleteApps(id);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }








        /*
        public IActionResult CreateItem(ItemDto item, [FromQuery] DateTime date)
        {
            ItemResult result = new ItemResult()
            {
                Id = Guid.NewGuid(),
                Author = item.AuthorId,
                Count = item.Count
            };

            return Ok(result);
        }
        */
    }
}

    /*
    public class ItemDto
    {
        public string ActivityType = Domain.ActivityType.ReportType; 
        public Guid AuthorId { get; init; }
        public int Count { get; init; }
    }
    
    public class ItemResult
    {
        public Guid Id { get; init; }
        public Guid Author { get; init; }
        public int Count { get; init; }
    }
}
    */
