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

namespace TimesheetSystem.Controllers.Tests
{
    [TestFixture()]
    public class TimesheetTests
    {
        private TimesheetDB _context;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TimesheetDB>()
                .UseInMemoryDatabase(databaseName: "TestTimesheetSystem")
                .Options;

            _context = new TimesheetDB(options);

            _context.Database.EnsureCreated();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
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
        [TestCase(3, "Project Alpha")]
        [TestCase(3, "Project Beta")]
        [TestCase(3, "Project Gamma")]
        public void DefaultSheetsAddedTest(int count, string projectName)
        {
            var users = _context.Timesheets.ToList();
            Assert.That(users.Count, Is.EqualTo(count), $"Count is supposed to be {count}");
            Assert.IsTrue(users.Any(u => u.Project == projectName), $"Project name {projectName} should be present.");
        }

        [Test(), Order(4)]
        [TestCase(2, "Alpha")]
        [TestCase(1, "Beta")]
        [TestCase(5, "Gamma")]

        public void DefaultSheetsFailedTest(int count, string projectName)
        {
            var users = _context.Timesheets.ToList();
            Assert.That(count, Is.Not.EqualTo(users.Count), $"Count is not supposed to be {count}");
            Assert.IsFalse(users.Any(u => u.Project == projectName), $"Project name {projectName} should not be present.");
        }
        [Test(), Order(3)]
        public void AddEntryTest()
        {
            Assert.Fail();
        }

        [Test(), Order(4)]
        public void EditEntryTest()
        {
            Assert.Fail();
        }

        [Test(), Order(5)]
        public void DeleteEntryTest()
        {
            Assert.Fail();
        }

        [Test(), Order(6)]

        public void DownloadEntriesTest()
        {
            Assert.Fail();
        }
    }
}