using zad5.Models;
using Microsoft.EntityFrameworkCore;

namespace zad5.Data;

public class PrescriptionDbContext : DbContext {
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public PrescriptionDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        
        modelBuilder.Entity<Patient>().HasData(new Patient
        {
            IdPatient = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            BirthDate = new DateTime(1980, 5, 12)
        });

       
        modelBuilder.Entity<Doctor>().HasData(new Doctor
        {
            IdDoctor = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Email = "anna.nowak@szpital.pl"
        });

        
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Przeciwbólowy",
                Type = "Tabletki"
            },
            new Medicament
            {
                IdMedicament = 2,
                Name = "Ibuprofen",
                Description = "Przeciwzapalny",
                Type = "Tabletki"
            });

        modelBuilder.Entity<Prescription>().HasData(new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2024, 1, 10),
            DueDate = new DateTime(2024, 2, 10),
            IdPatient = 1,
            IdDoctor = 1
        });

        
        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 2,
                Description = "Rano i wieczorem"
            },
            new
            {
                IdPrescription = 1,
                IdMedicament = 2,
                Dose = 1,
                Description = "Po posiłku"
            });
    }

}

