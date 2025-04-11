namespace TimesheetSystem.Models
{
    //Add list of users on DB set up with a uniqueID to filter the total hours
    public class UserData
    {
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

    }
}
