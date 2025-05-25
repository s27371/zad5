using zad5.DTOs;
using zad5.Services;

namespace zad5.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionRequest request)
    {
        try
        {
            await _service.AddPrescriptionAsync(request);
            return Ok("Prescription added successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
