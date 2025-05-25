namespace zad5.DTOs;

using System;
using System.Collections.Generic;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public List<MedicamentDto> Medicaments { get; set; }

    public DoctorDto Doctor { get; set; }
}
