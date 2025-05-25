namespace zad5.DTOs;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CreatePrescriptionRequest
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public PatientDto Patient { get; set; }

    [Required]
    public DoctorDto Doctor { get; set; }

    [Required]
    [MaxLength(10)]
    public List<MedicamentDto> Medicaments { get; set; }
}
