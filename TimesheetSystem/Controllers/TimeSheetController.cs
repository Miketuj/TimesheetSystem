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
        public TimesheetController(TimesheetDB context)
        {
            _context = context;
        }

        public IActionResult Timesheet()
        {
            ViewBag.Users = _context.Users;
            return View();
        }
        [HttpPost("entry/add")]
        public IActionResult AddEntry(TimesheetData timesheet)
        {
            try
            {
                _context.Timesheets.Add(timesheet);
                _context.SaveChanges();
                return Ok(new { StatusMessage = "Timesheet was added" });
            }
            catch (Exception)
            {
                return BadRequest(new { StatusMessage = "There was an error" });
            }
        }

        [HttpGet("download/csv")]
        public async Task<IActionResult> DownloadEntries()
        {
            try
            {
                var Timesheets = await _context.Timesheets.ToListAsync();
                var users = await _context.Users.ToListAsync();
                var TimesheetData = TimesheetFunctions.ReturnTimesheetData(users, Timesheets);
                var bytes = TimesheetData.ReturnCSVBytes();
                var test = File(bytes, "text/csv", "timesheets.csv");
                return test;
            }
            catch (Exception)
            {
                return BadRequest(new { StatusMessage = "There was an error" });
            }
              

           
        }

    }
}
