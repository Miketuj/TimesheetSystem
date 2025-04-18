using Microsoft.EntityFrameworkCore;

namespace TimesheetSystem.Models
{
 public class TimesheetDB : DbContext
    {
        public TimesheetDB(DbContextOptions<TimesheetDB> options) :base(options) { }
        public DbSet<UserData> Users { get;set; }
        public DbSet<TimesheetData> Timesheets { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserData>()
                .HasKey(ut => ut.Id);

            modelBuilder.Entity<TimesheetData>()
                .HasOne(ut => ut.UserData)
                .WithMany(u => u.Timesheets)
                .HasForeignKey(ut => ut.UserID);

            modelBuilder.Entity<UserData>().HasData(
                new UserData() { Id = 1, UserName = "John Smith" },
                new UserData() { Id = 2, UserName = "Jane Doe" }

            );
            var date = DateTime.UtcNow;
            modelBuilder.Entity<TimesheetData>().HasData(
              new TimesheetData { Id = 1, UserID = 1, Date = date, HoursWorked = 4, Project = "Project Alpha", Description = "Developed new feature X" },
              new TimesheetData { Id = 2, UserID = 1, Date = date, HoursWorked = 4, Project = "Project Beta", Description = "Fixed bugs in module Y" },
              new TimesheetData { Id = 3, UserID = 2, Date = date, HoursWorked = 6, Project = "Project Gamma", Description = "Conducted user testing" },
              new TimesheetData { Id = 4, UserID = 1, Date = date.AddDays(-1), HoursWorked = 4, Project = "Project Beta", Description = "Fixed bugs in module Z" },
              new TimesheetData { Id = 5, UserID = 2, Date = date.AddDays(-1), HoursWorked = 4, Project = "Project Beta", Description = "Fixed bugs in module X" }
          );
            base.OnModelCreating(modelBuilder);

        }
    }
}
