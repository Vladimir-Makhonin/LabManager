using LabManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LabManager.Data
{
    public class LabManagerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public LabManagerDbContext(DbContextOptions<LabManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Experiment> Experiments { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }


        /// <summary>
        /// Configures entity relationships and database constraints
        /// that are not infered by Entity Framework Core automatically.
        /// </summary>
        /// <param name="modelBuilder">
        /// The object used to configure entities and their relationships.
        /// </param>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Applies any configuration defined by the base DbContext class.
            base.OnModelCreating(modelBuilder);


            // Configures a composite primary key for the junction entity.
            // The combination of ExperimentId and EquipmentId uniquely identifies
            // each assignment and prevents the same equipment from being assigned
            // to the same experiment more than once.
            modelBuilder.Entity<ExperimentEquipment>()
                .HasKey(experimentEquipment => new
                {
                    experimentEquipment.ExperimentId,
                    experimentEquipment.EquipmentId
                });


            // Configures the relationship between ExperimentEquipment and Experiment.
            // Each junction record belongs to one experiment,
            // while one experiment can have many junction records.
            modelBuilder.Entity<ExperimentEquipment>()
                .HasOne(experimentEquipment => experimentEquipment.Experiment)
                .WithMany(experiment => experiment.ExperimentEquipments)
                .HasForeignKey(experimentEquipment =>
                    experimentEquipment.ExperimentId);


            // Configures the relationship between ExperimentEquipment and Equipment.
            // Each junction record belongs to one piece of equipment,
            // while one piece of equipment can have many junction records.
            modelBuilder.Entity<ExperimentEquipment>()
                .HasOne(experimentEquipment => experimentEquipment.Equipment)
                .WithMany(equipment => equipment.ExperimentEquipments)
                .HasForeignKey(experimentEquipment =>
                    experimentEquipment.EquipmentId);
        }
    }
}