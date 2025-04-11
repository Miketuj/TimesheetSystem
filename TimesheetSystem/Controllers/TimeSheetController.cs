using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetSystem.Functions;
using TimesheetSystem.Models;

namespace TimesheetSystem.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly TimesheetDB _context;
        //Timesheet form page
        public TimesheetController(TimesheetDB context)
        {
            _context = context;
        }

        public IActionResult Timesheet()
        {
            return View();
        }
        //Add entry
        [HttpPost("entry/add")]
        public IActionResult AddEntry(TimesheetData Timesheet)
        {
            try
            {
                _context.Timesheets.Add(Timesheet);
                _context.SaveChanges();
                return Ok(new { StatusMessage = "Timesheet was added" });
            }
            catch (Exception)
            {

                return BadRequest(new { StatusMessage = "There was an error" });

            }

        }

        //Download report

        [HttpPost("download/csv")]
        public async Task<IActionResult> DownloadEntries()
        {
            try
            {
                var Timesheets = await _context.Timesheets.ToListAsync();
                var users = await _context.Users.ToListAsync();
                var TimesheetData = TimesheetFunctions.ReturnTimesheetData(users, Timesheets);
                var bytes = TimesheetData.ReturnCSVBytes();

                return File(bytes, "text/csv", "users.csv");
            }
            catch (Exception)
            {
                return BadRequest(new { StatusMessage = "There was an error" });
            }
              

           
        }

    }
}
