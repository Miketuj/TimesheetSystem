using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetSystem.Models;

namespace TimesheetSystem.Functions
{
    public static class TimesheetFunctions
    {

        public static List<TimesheetDTO> ReturnTimesheetData(List<UserData> users, List<TimesheetData> Timesheets)
        {
            var TimesheetData = new List<TimesheetDTO>();

            //Timesheets groupby the userid
            //Filter by the Date
            //Count up the hours that specific day
            
            //Create a new Timesheet entity with the total hours of that day on there
            //Match to a user, add the userName
            //Add to TimesheetData

            return TimesheetData;
        }

        public static byte[] ReturnCSVBytes(this List<TimesheetDTO> TimesheetData)
        {
            var csv = new StringBuilder();
            csv.AppendLine("User Name,Date, Project, Description of Tasks, Hours Worked, Total Hours for the Day");

            foreach (var Timesheet in TimesheetData)
            {
                csv.AppendLine($"{Timesheet.UserName}," +
                    $"{Timesheet.Date}," +
                    $"{Timesheet.Description}," +
                    $"{Timesheet.HoursWorked}," +
                    $"{Timesheet.TotalHours}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
