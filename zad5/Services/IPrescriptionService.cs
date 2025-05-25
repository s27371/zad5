using zad5.DTOs;

namespace zad5.Services;

using System.Threading.Tasks;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(CreatePrescriptionRequest request);
    Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient);
}
