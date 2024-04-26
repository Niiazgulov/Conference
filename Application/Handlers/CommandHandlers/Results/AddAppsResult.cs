using Domain.Models;

namespace Application.Handlers.CommandHandlers.Results
{
    public class AddAppsResult
    {
        public bool Result {  get; set; }
        public string? Message { get; set; }
        public Applications? Newapp {  get; set; }
    }
}
