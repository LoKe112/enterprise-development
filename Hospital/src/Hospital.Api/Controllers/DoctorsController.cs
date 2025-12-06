using Hospital.Application.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Application.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;
namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController(ILogger<DoctorsController> logger, IDoctorService service) : ControllerBase
{
    private readonly ILogger<DoctorsController> _logger = logger;
    private readonly IDoctorService _service = service;

    /// <summary>Returns all doctors.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<DoctorResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in DoctorController");
        try
        {
            List<DoctorResponse> doctors = await _service.GetAllDoctorsAsync();
            return Ok(doctors);
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
            DoctorResponse? doctor = await _service.GetDoctorAsync(id);
            if (doctor is null)
            {
                return NotFound();
            }
            return Ok(doctor);
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
            DoctorResponse entity = await _service.CreateDoctorAsync(doctorDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
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
            DoctorResponse? updated = await _service.UpdateDoctorAsync(id, doctorDto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
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
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
