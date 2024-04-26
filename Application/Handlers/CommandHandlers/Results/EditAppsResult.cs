using Domain.Models;

namespace Application.Handlers.CommandHandlers.Results
{
    public class EditAppsResult
    {
        public bool Result { get; set; }
        public string? Message { get; set; }
        public Applications? Editedapp { get; set; }
    }
}
