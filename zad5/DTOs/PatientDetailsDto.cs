namespace zad5.DTOs;

using System;
using System.Collections.Generic;

public class PatientDetailsDto
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public List<PrescriptionDto> Prescriptions { get; set; }
}
