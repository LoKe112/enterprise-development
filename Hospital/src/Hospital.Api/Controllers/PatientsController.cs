using Hospital.Application.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController(ILogger<PatientsController> logger, IPatientService service) : ControllerBase
{
    private readonly ILogger<PatientsController> _logger = logger;
    private readonly IPatientService _service = service;

    /// <summary>Returns all patients.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PatientResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in PatientController");
        try
        {
            var patients = await _service.GetAllPatientsAsync();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a Patient by Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientResponse>> GetById(Guid id)
    {
        _logger.LogInformation("Called GetById in PatientController");
        try
        {
            var patient = await _service.GetPatientAsync(id);
            if (patient is null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new Patient.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] PatientRequest PatientDto)
    {
        _logger.LogInformation("Called Create in PatientController");

        if (PatientDto is null)
        {
            return BadRequest("Patient data is required.");
        }

        try
        {
            var entity = await _service.CreatePatientAsync(PatientDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates a Patient by Id.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientResponse>> Update(Guid id, [FromBody] PatientRequest PatientDto)
    {
        _logger.LogInformation("Called Update in PatientController");

        if (PatientDto is null)
        {
            return BadRequest("Patient data is required.");
        }

        try
        {
            var updated = await _service.UpdatePatientAsync(id, PatientDto);
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

    /// <summary>Deletes a Patient by Id.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Called Delete in PatientController");

        try
        {
            var deleted = await _service.DeletePatientAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
