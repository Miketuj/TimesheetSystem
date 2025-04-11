using NUnit.Framework;
using TimesheetSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TimesheetSystem.Controllers.Tests
{
    [TestFixture()]
    public class TimesheetTests
    {
        [SetUp]
        public void SetUp()
        {
            //Set up the db and add some test data for the edit, delete & download
 
        }

        [Test()]
        public void AddEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DownloadEntriesTest()
        {
            Assert.Fail();
        }
    }
}