using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetSystem.Models;

namespace TimesheetSystem.Functions
{
    public static class TimeSheetFunctions
    {

        public static List<TimesheetData> ReturnTimesheetData(List<UserData> users, List<TimesheetData> timesheets)
        {
            var timesheetData = new List<TimesheetData>();

            //timesheets groupby the userid
            //Filter by the Date
            //Count up the hours that specific day
            
            //Create a new timesheet entity with the total hours of that day on there
            //Match to a user, add the userName
            //Add to timesheetData

            return timesheetData;
        }

        public static byte[] ReturnCSVBytes(this List<TimesheetData> timesheetData)
        {
            
            var csv = new StringBuilder();
            csv.AppendLine("User Name,Date, Project, Description of Tasks, Hours Worked, Total Hours for the Day");

            foreach (var timesheet in timesheetData)
            {
                csv.AppendLine($"{timesheet.UserData!.UserName}," +
                    $"{timesheet.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)}," +
                    $"{timesheet.Project}," +
                    $"{timesheet.Description}," +
                    $"{timesheet.HoursWorked}," +
                    $"{timesheet.TotalHours}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
