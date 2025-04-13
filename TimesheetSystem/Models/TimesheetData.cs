using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
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
