namespace TimesheetSystem.Models
{
    //Add list of users on DB set up with a uniqueID to filter the total hours
    public class TimesheetData
    {
        public string EntryID { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Project { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int HoursWorked { get; set; }
        public int TotalHours { get; set; }

    }
}
