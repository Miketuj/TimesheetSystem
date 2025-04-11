using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    //Add list of users on DB set up with a uniqueID to filter the total hours
    public class TimesheetDTO
    {
        public string Date { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double HoursWorked { get; set; }
        public double TotalHours { get; set; }
        public string UserName { get; set; } = string.Empty;


    }
}
