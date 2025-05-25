using Microsoft.EntityFrameworkCore;

namespace zad5.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PrescriptionMedicament")]
[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))]
public class PrescriptionMedicament
{
    
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }
    
    
    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }

    [Required, Range(1, 1000)]
    public int Dose { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }
}
