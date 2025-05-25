namespace zad5.DTOs;

using System;
using System.ComponentModel.DataAnnotations;

public class PatientDto
{
    public int? IdPatient { get; set; }  // może być null, jeśli pacjent nowy

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
}
