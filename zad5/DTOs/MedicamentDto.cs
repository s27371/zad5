namespace zad5.DTOs;

using System.ComponentModel.DataAnnotations;

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }  // tylko dla odczytu

    [Required]
    public int Dose { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }
}
