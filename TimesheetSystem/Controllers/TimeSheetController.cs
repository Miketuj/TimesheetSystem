using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetSystem.Models;

namespace TimesheetSystem.Controllers
{
    public class TimeSheetController : Controller
    {
        //Timesheet form page
        public IActionResult Timesheet()
        {
            return View();
        }
        //Add entry
        [HttpPost]
        public IActionResult AddEntry(TimesheetData user)
        {
            //Check valid data
            return BadRequest();
        }
        //Edit entry
        [HttpPost]
        public IActionResult EditEntry(TimesheetData user)
        {
            //Check valid data
            return BadRequest();
        }
        //Delete entry
        [HttpPost]
        public IActionResult DeleteEntry(TimesheetData user)
        {
            //Check valid data
            return BadRequest();
        }
        //Download report

        [HttpPost]
        public IActionResult DownloadEntries()
        {
            //Check valid data
            return BadRequest();
        }

    }
}
