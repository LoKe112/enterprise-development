using Hospital.Api.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Domain.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;
namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly ILogger<DoctorsController> _logger;
    private readonly IDoctorService _service;

    public DoctorsController(ILogger<DoctorsController> logger, IDoctorService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>Returns all doctors.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<DoctorResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in DoctorController");
        try
        {
            List<Doctor> doctors = await _service.GetAllDoctorsAsync();
            var response = doctors.Select(s => s.ToResponse()).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a doctor by Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorResponse>> GetById(Guid id)
    {
        _logger.LogInformation("Called GetById in DoctorController");
        try
        {
            Doctor? doctor = await _service.GetDoctorAsync(id);
            if (doctor is null)
            {
                return NotFound();
            }
            return Ok(doctor.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new doctor.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] DoctorRequest doctorDto)
    {
        _logger.LogInformation("Called Create in DoctorController");

        if (doctorDto is null)
        {
            return BadRequest("Doctor data is required.");
        }

        try
        {
            Doctor entity = await _service.CreateDoctorAsync(doctorDto.ToDomain());
            return CreatedAtAction(nameof(GetById), new
            {
                entity.Id
            }, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates a doctor by Id.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorResponse>> Update(Guid id, [FromBody] DoctorRequest doctorDto)
    {
        _logger.LogInformation("Called Update in DoctorController");

        if (doctorDto is null)
        {
            return BadRequest("Doctor data is required.");
        }

        try
        {
            Doctor doctorToUpdate = doctorDto.ToDomain();

            Doctor? updated = await _service.UpdateDoctorAsync(id, doctorToUpdate);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Update");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Deletes a doctor by Id.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Called Delete in DoctorController");

        try
        {
            var deleted = await _service.DeleteDoctorAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
