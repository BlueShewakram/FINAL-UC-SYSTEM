using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(s => s.Id)
                .UseIdentityColumn(1, 1);
        }

        public async Task ResequenceStudentIds()
        {
            var students = await Students.OrderBy(s => s.Id).ToListAsync();
            for (int i = 0; i < students.Count; i++)
            {
                students[i].Id = i + 1;
            }
            await SaveChangesAsync();
            
            // Reset the identity to the next available number
            var nextId = students.Count > 0 ? students.Count + 1 : 1;
#pragma warning disable EF1002 // Risk of SQL injection
            await Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Students', RESEED, {0})", students.Count);
#pragma warning restore EF1002 // Risk of SQL injection
        }

        public DbSet<Student> Students {get; set;}
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<LockerRequest> LockerRequests { get; set; }  
        public DbSet<GatePassModel> GatePasses { get; set; }
        }
}
