namespace zad5.DTOs;

using System.ComponentModel.DataAnnotations;

public class DoctorDto
{
    [Required]
    public int IdDoctor { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
