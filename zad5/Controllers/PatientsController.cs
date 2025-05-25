using zad5.Services;

namespace zad5.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PatientsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var result = await _service.GetPatientDetailsAsync(id);
        if (result == null)
        {
            return NotFound($"Patient with ID {id} not found.");
        }

        return Ok(result);
    }
}
