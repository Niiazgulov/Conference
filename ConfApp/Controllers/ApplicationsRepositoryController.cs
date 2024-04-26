using Microsoft.AspNetCore.Mvc;
using Application.Handlers.Contracts.CommandHandlers;
using Domain.Models;

namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsRepositoryController : ControllerBase
    {
        private IEditAppsHandler _editAppsHandler;
        private IAddNewApplicationHandler _addNewApplicationHandler;
        private IDeleteAppsHandler _deleteAppsHandler;
        private IAddAppsToReviewHandler _addAppsToReviewHandler;

        public ApplicationsRepositoryController(IEditAppsHandler editAppsHandler, IAddNewApplicationHandler addNewApplicationHandler, IDeleteAppsHandler deleteAppsHandler, IAddAppsToReviewHandler addAppsToReviewHandler)
        {
            _editAppsHandler = editAppsHandler;
            _addNewApplicationHandler = addNewApplicationHandler;
            _deleteAppsHandler = deleteAppsHandler;
            _addAppsToReviewHandler = addAppsToReviewHandler;
        }

        [HttpPost("applications")]
        public async Task<IActionResult> AddApp(NewAppDTO app)
        {
            try
            {
                var addAppsResult = await _addNewApplicationHandler.AddApps(app);
                if (addAppsResult.Result)
                {
                    return Ok(addAppsResult.Newapp);
                }
                return StatusCode(400, addAppsResult.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditApp(Guid id, EditedAppDTO app)
        {
            try
            {
                var editAppsResult = await _editAppsHandler.EditApps(id, app);
                if (editAppsResult.Result)
                {
                    return Ok(editAppsResult.Editedapp);
                }
                return StatusCode(400, editAppsResult.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(Guid id)
        {
            try
            {
                var deleteAppsResult = await _deleteAppsHandler.DeleteApps(id);
                if (deleteAppsResult.Result)
                {
                    return Ok();
                }
                return StatusCode(400, deleteAppsResult.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("applications/{id}/submit")]
        public async Task<IActionResult> AddAppToReview(Guid id)
        {
            try
            {
                var reviewAddAppsResult = await _addAppsToReviewHandler.AddAppsToReview(id);
                if (reviewAddAppsResult.Result)
                {
                    return Ok();
                }
                return StatusCode(400, reviewAddAppsResult.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}