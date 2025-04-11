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
using System.Globalization;
using Microsoft.AspNetCore.Rewrite;
using System.Text.RegularExpressions;

namespace TimesheetSystem.Controllers.Tests
{
    [TestFixture()]
    public class TimesheetTests
    {
        private TimesheetDB _context;
        private TimesheetController _controller;
        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TimesheetDB>()
                .UseInMemoryDatabase(databaseName: "TestTimesheetSystem")
                .Options;

            _context = new TimesheetDB(options);

            _context.Database.EnsureCreated();
            _controller = new TimesheetController(_context);
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
        public async Task DefaultUsersAddedTest(int count, string user)
        {
            var users = await _context.Users.ToListAsync();
            Assert.That(users.Count, Is.EqualTo(count), $"Count is supposed to be {count}");
            Assert.IsTrue(users.Any(u => u.UserName == user), $"User {user} should be present.");
        }
        [Test(), Order(2)]
        [TestCase(3, "John")]
        [TestCase(1, "Jane")]
        [TestCase(5, "Paul")]

        public async Task DefaultUsersAddedFailedTest(int count, string user)
        {
            var users = await _context.Users.ToListAsync();
            Assert.That(count, Is.Not.EqualTo(users.Count), $"Count is not supposed to be {count}");
            Assert.IsFalse(users.Any(u => u.UserName == user), $"User {user} should not be present.");
        }
        [Test(), Order(3)]
        [TestCase(5, "Project Alpha")]
        [TestCase(5, "Project Beta")]
        [TestCase(5, "Project Gamma")]
        public async Task DefaultSheetsAddedTest(int count, string projectName)
        {
            var timesheets = await _context.Timesheets.ToListAsync();
            Assert.That(timesheets.Count, Is.EqualTo(count), $"Count is supposed to be {count}");
            Assert.IsTrue(timesheets.Any(u => u.Project == projectName), $"Project name {projectName} should be present.");
        }

        [Test(), Order(4)]
        [TestCase(2, "Alpha")]
        [TestCase(1, "Beta")]
        [TestCase(0, "Gamma")]

        public async Task DefaultSheetsFailedTest(int count, string projectName)
        {
            var timesheets = await _context.Timesheets.ToListAsync();
            Assert.That(count, Is.Not.EqualTo(timesheets.Count), $"Count is not supposed to be {count}");
            Assert.IsFalse(timesheets.Any(u => u.Project == projectName), $"Project name {projectName} should not be present.");
        }
        [Test(), Order(3)]
        public async Task AddEntryTest()
        {
            var originalSheets = await _context.Timesheets.ToListAsync();
            var count = originalSheets.Count + 1;

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

            var timesheets = await _context.Timesheets.ToListAsync();
            Assert.That(count, Is.EqualTo(timesheets.Count), $"Count supposed to be {count}");
            Assert.IsNotNull(timesheets.Find(c => c.Id == entry.Id));

        }


        [Test(), Order(4)]

        public void ReturnCSVBytes()
        {
            var timesheetData = new List<TimesheetDTO>() 
            {
                   new TimesheetDTO()
                    {
                        UserName = "TEST",
                        Date = "11/04/2025",
                        Project = "Project",
                        Description = "Description",
                        HoursWorked = 4,
                        TotalHours = 8
                    },
                   new TimesheetDTO()
                    {
                        UserName = "TEST",
                        Date = "11/04/2025",
                        Project = "Project2",
                        Description = "Description2",
                        HoursWorked = 4,
                        TotalHours = 8
                    },
                     new TimesheetDTO()
                    {
                        UserName = "TEST2",
                        Date = "11/04/2025",
                        Project = "Project2",
                        Description = "Description2",
                        HoursWorked = 4,
                        TotalHours = 4
                    },
            };
            var result = timesheetData.ReturnCSVBytes();

            var content = Encoding.UTF8.GetString(result);

            var lines = content.Split('\n');
            //Header & trailing row counted
            Assert.IsTrue(lines.Length == timesheetData.Count + 2);

        }
        [Test(), Order(5)]
        public async Task ReturnTimesheetDataTest()
        {
            var timesheets = await _context.Timesheets.ToListAsync();
            var users = await _context.Users.ToListAsync();

            var result = TimesheetFunctions.ReturnTimesheetData(users, timesheets);

            Assert.That(timesheets.Count, Is.EqualTo(result.Count));
            foreach (var item in result)
            {
                Assert.That(item.TotalHours, Is.Not.EqualTo(0));
            }
        }


    }
}