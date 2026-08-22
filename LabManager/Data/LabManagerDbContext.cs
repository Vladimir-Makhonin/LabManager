using LabManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Data
{
    public class LabManagerDbContext : DbContext
    {
        public LabManagerDbContext(DbContextOptions<LabManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Experiment> Experiments { get; set; }

        public DbSet<Equipment> Equipment { get; set; }
    }
}