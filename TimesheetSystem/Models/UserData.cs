using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<TimesheetData> Timesheets { get; set; } = [];
    }
}
