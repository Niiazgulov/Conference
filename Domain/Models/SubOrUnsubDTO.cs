using Microsoft.AspNetCore.Mvc;

namespace Domain.Models
{
    public class SubOrUnsubDTO
    {
        [FromQuery]
        public DateTime? unsubmittedOlder { get; set; }
        [FromQuery]
        public DateTime? submittedAfter { get; set; }
    }
}
