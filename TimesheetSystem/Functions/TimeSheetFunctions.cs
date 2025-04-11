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

        public static List<TimesheetDTO> ReturnTimesheetData(List<UserData> users, List<TimesheetData> timesheets)
        {
            var grouped = timesheets.GroupBy(c => new { c.Date.Date, c.UserID }).ToList();
            var timesheetData = new List<TimesheetDTO>();
            foreach (var entries in grouped)
            {
                var totalHours = entries.Sum(c => c.HoursWorked);
                var userdailyEntries = entries.Select(c =>
                new TimesheetDTO()
                {
                    UserName = c.UserData.UserName,
                    Date = c.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    Project = c.Project,
                    Description = c.Description,
                    HoursWorked = c.HoursWorked,
                    TotalHours = totalHours
                });
                timesheetData.AddRange(userdailyEntries);
            }

            return timesheetData;
        }

        public static byte[] ReturnCSVBytes(this List<TimesheetDTO> timesheetData)
        {
            var csv = new StringBuilder();
            csv.AppendLine("User Name,Date, Project, Description of Tasks, Hours Worked, Total Hours for the Day");

            foreach (var timesheet in timesheetData)
            {
                csv.AppendLine($"{timesheet.UserName}," +
                    $"{timesheet.Date}," +
                    $"{timesheet.Description}," +
                    $"{timesheet.HoursWorked}," +
                    $"{timesheet.TotalHours}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
