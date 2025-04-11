using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    //Add list of users on DB set up with a uniqueID to filter the total hours
    public class UserData
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<TimesheetData> Timesheets { get; set; } = [];
    }
}
