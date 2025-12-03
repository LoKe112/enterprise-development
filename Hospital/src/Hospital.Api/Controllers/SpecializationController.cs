using Hospital.Api.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Domain.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;
namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationController : ControllerBase
{
    private readonly ILogger<SpecializationController> _logger;
    private readonly ISpecializationService _service;

    public SpecializationController(ILogger<SpecializationController> logger, ISpecializationService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>Returns all specializations.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SpecializationResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in SpecializationController");
        try
        {
            List<Specialization> specializations = await _service.GetAllSpecializationsAsync();
            var response = specializations.Select(s => s.ToResponse()).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a specialization by Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationResponse>> GetById(Guid id)
    {
        _logger.LogInformation("Called GetById in SpecializationController");
        try
        {
            Specialization? specialization = await _service.GetSpecializationAsync(id);
            if (specialization is null)
            {
                return NotFound();
            }
            return Ok(specialization.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new specialization.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] SpecializationRequest specializationDto)
    {
        _logger.LogInformation("Called Create in SpecializationController");

        if (specializationDto is null)
        {
            return BadRequest("Specialization data is required.");
        }

        try
        {
            Specialization id = await _service.CreateSpecializationAsync(specializationDto.ToDomain());
            return CreatedAtAction(nameof(GetById), new
            {
                id
            }, null);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validation failed during Create");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates a specialization by Id.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationResponse>> Update(Guid id, [FromBody] SpecializationRequest specializationDto)
    {
        _logger.LogInformation("Called Update in SpecializationController");

        if (specializationDto is null)
        {
            return BadRequest("Specialization data is required.");
        }

        try
        {
            Specialization specializationToUpdate = specializationDto.ToDomain();

            Specialization? updated = await _service.UpdateSpecializationAsync(id, specializationToUpdate);
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

    /// <summary>Deletes a specialization by Id.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Called Delete in SpecializationController");

        try
        {
            var deleted = await _service.DeleteSpecializationAsync(id);
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
