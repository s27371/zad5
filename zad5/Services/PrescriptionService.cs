using zad5.Data;
using zad5.DTOs;
using zad5.Models;

namespace zad5.Services;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


public class PrescriptionService : IPrescriptionService
{
    private readonly PrescriptionDbContext _context;

    public PrescriptionService(PrescriptionDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(CreatePrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("A prescription cannot contain more than 10 medicaments.");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate must be greater than or equal to Date.");

        var allMedicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => allMedicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        if (existingMedicaments.Count != allMedicamentIds.Count)
            throw new InvalidOperationException("Some medicaments do not exist in the database.");

        // Find or add patient
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.FirstName == request.Patient.FirstName &&
                                      p.LastName == request.Patient.LastName &&
                                      p.BirthDate == request.Patient.BirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Check or create doctor (optionally you can validate instead)
        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null)
            throw new InvalidOperationException($"Doctor with ID {request.Doctor.IdDoctor} does not exist.");

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Description = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Where(p => p.IdPatient == idPatient)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync();

        if (patient == null)
            return null;

        return new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Description
                    }).ToList()
                }).ToList()
        };
    }
}
