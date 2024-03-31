using Microsoft.AspNetCore.Mvc;
using Domain;
using Domain.Handlers.Contracts;


namespace ConfApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsRepositoryController : ControllerBase
    {
        private IEditAppsHandler _editAppsHandler;
        private IAddNewApplicationHandler _addNewApplicationHandler;
        private IGetAppsByIdHandler _getAppsByIdHandler;
        private IDeleteAppsHandler _deleteAppsHandler;
        private ICheckSendedHandler _checkSendedHandler;
        private IAddAppsToReviewHandler _addAppsToReviewHandler;

        public ApplicationsRepositoryController(IEditAppsHandler editAppsHandler, IAddNewApplicationHandler addNewApplicationHandler, IGetAppsByIdHandler getAppsByIdHandler, IDeleteAppsHandler deleteAppsHandler, ICheckSendedHandler checkSendedHandler, IAddAppsToReviewHandler addAppsToReviewHandler)
        {
            _editAppsHandler = editAppsHandler;
            _addNewApplicationHandler = addNewApplicationHandler;
            _getAppsByIdHandler = getAppsByIdHandler;
            _deleteAppsHandler = deleteAppsHandler;
            _checkSendedHandler = checkSendedHandler;
            _addAppsToReviewHandler = addAppsToReviewHandler;
        }

        [HttpPost("applications")]
        public async Task<IActionResult> CreateApp(NewAppDTO app)
        {
            try
            {
                if (app.Author.ToString() == "00000000-0000-0000-0000-000000000000")
                    return StatusCode(400, "Укажите идентификатор автора в формате Guid!");

                var newapp = await _addNewApplicationHandler.AddApps(app);
                if (newapp == null)
                    return NotFound();
                if (newapp.Author.ToString() == "00000000-0000-0000-0000-000000000001")
                    return StatusCode(400, "У этого автора уже есть 1 заявка в черновиках, больше добавить невозможно.");
                if (newapp.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                    return StatusCode(400, "Заполните еще хотя бы 1 поле в черновике заявки.");

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
                var dbApp = await _getAppsByIdHandler.GetAppsById(id);
                if (dbApp == null)
                    return NotFound();
                var sended = await _checkSendedHandler.CheckSended(id);
                if (sended == "YES")
                    return StatusCode(400, "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.");

                var editedapp = await _editAppsHandler.EditApps(id, app);
                if (editedapp == null)
                    return NotFound();

                return Ok(editedapp);
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
                var dbApp = await _getAppsByIdHandler.GetAppsById(id);
                if (dbApp == null)
                    return NotFound();
                var sended = await _checkSendedHandler.CheckSended(id);
                if (sended == "YES")
                    return StatusCode(400, "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.");

                await _deleteAppsHandler.DeleteApps(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

        [HttpPost("applications/{id}/submit")]
        public async Task<IActionResult> AddAppsToReview(Guid id)
        {
            try
            {
                var dbApp = await _getAppsByIdHandler.GetAppsById(id);
                if (dbApp == null)
                    return NotFound();
                string result = await _addAppsToReviewHandler.AddAppsToReview(id);
                if (result == "Success")
                    return Ok();
                return StatusCode(400, "В черновике заявки есть поле NULL! Заявка не может быть отправлена, добавьте недостающие данные.");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}