using NUnit.Framework;
using TimesheetSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetSystem.Models;
using Microsoft.AspNetCore.Hosting;
using TimesheetSystem.Functions;

namespace TimesheetSystem.Controllers.Tests
{
    [TestFixture()]
    public class TimesheetTests
    {
        private TimesheetDB _context;
        private TimeSheetController _controller;
        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TimesheetDB>()
                .UseInMemoryDatabase(databaseName: "TestTimesheetSystem")
                .Options;

            _context = new TimesheetDB(options);

            _context.Database.EnsureCreated();
            _controller = new TimeSheetController(_context);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            _controller.Dispose();
        }

        [Test(), Order(1)]
        [TestCase(2, "John Smith")]
        [TestCase(2, "Jane Doe")]
        public void DefaultUsersAddedTest(int count, string user)
        {
            var users = _context.Users.ToList();
            Assert.That(users.Count, Is.EqualTo(count), $"Count is supposed to be {count}");
            Assert.IsTrue(users.Any(u => u.UserName == user), $"User {user} should be present.");
        }
        [Test(), Order(2)]
        [TestCase(3, "John")]
        [TestCase(1, "Jane")]
        [TestCase(5, "Paul")]

        public void DefaultUsersAddedFailedTest(int count, string user)
        {
            var users = _context.Users.ToList();
            Assert.That(count, Is.Not.EqualTo(users.Count), $"Count is not supposed to be {count}");
            Assert.IsFalse(users.Any(u => u.UserName == user), $"User {user} should not be present.");
        }
        [Test(), Order(3)]
        [TestCase(5, "Project Alpha")]
        [TestCase(5, "Project Beta")]
        [TestCase(5, "Project Gamma")]
        public void DefaultSheetsAddedTest(int count, string projectName)
        {
            var timesheets = _context.Timesheets.ToList();
            Assert.That(timesheets.Count, Is.EqualTo(count), $"Count is supposed to be {count}");
            Assert.IsTrue(timesheets.Any(u => u.Project == projectName), $"Project name {projectName} should be present.");
        }

        [Test(), Order(4)]
        [TestCase(2, "Alpha")]
        [TestCase(1, "Beta")]
        [TestCase(0, "Gamma")]

        public void DefaultSheetsFailedTest(int count, string projectName)
        {
            var timesheets = _context.Timesheets.ToList();
            Assert.That(count, Is.Not.EqualTo(timesheets.Count), $"Count is not supposed to be {count}");
            Assert.IsFalse(timesheets.Any(u => u.Project == projectName), $"Project name {projectName} should not be present.");
        }
        [Test(), Order(3)]
        public void AddEntryTest()
        {
            var count = _context.Timesheets.ToList().Count + 1;

            var entry = new TimesheetData()
            {
                Id = 6,
                Date = DateTime.Now,
                UserID = 1,
                Project = "Test",
                Description = "Test description",
                HoursWorked = 8,
            };
           var result = _controller.AddEntry(entry);

            var timesheets = _context.Timesheets.ToList();
            Assert.That(count, Is.EqualTo(timesheets.Count), $"Count supposed to be {count}");
            Assert.IsNotNull(timesheets.Find(c=>c.Id == entry.Id));

        }


        [Test(), Order(4)]

        public void ReturnCSVBytes()
        {
            List<TimesheetData> timesheetData = new List<TimesheetData>();

            var result = timesheetData.ReturnCSVBytes();

 

        }
        [Test(), Order(5)]
        public async Task ReturnTimesheetDataTest()
        {
            var timesheets = await _context.Timesheets.ToListAsync();
            var users = await _context.Users.ToListAsync();

      

            Assert.Fail();
        }


    }
}