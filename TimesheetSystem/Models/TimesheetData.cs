using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    //Add list of users on DB set up with a uniqueID to filter the total hours
    public class TimesheetData
    {
        public int Id{ get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double HoursWorked { get; set; }
        public double TotalHours { get; set; }
        public UserData? UserData { get; set; }


    }
}
