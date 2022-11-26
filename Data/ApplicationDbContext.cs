using Microsoft.EntityFrameworkCore;
using Homework_Log.Models;

// this class is used to create The DI container i believe and is added to the pipe line in Program.cs
namespace Homework_Log.Data {
	public class ApplicationDbContext :DbContext {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) {
			
		}
		// add models here 
		public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

    }
}
