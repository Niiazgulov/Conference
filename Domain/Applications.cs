using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Applications
    {
        public Guid Id{ get; set; }
        public Guid Author { get; set; }
        public string? Activity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Outline { get; set; }
    }

    /*
    public static class ActivityType
    {
        public static string ReportType = "Report";
        public static string MasterClassType = "Masterclass";
        public static string DiscussionType = "Discussion";
    }
    */
}
