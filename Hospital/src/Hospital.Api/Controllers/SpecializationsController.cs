using Hospital.Application.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationsController(ILogger<SpecializationsController> logger, ISpecializationService service) : ControllerBase
{
    private readonly ILogger<SpecializationsController> _logger = logger;
    private readonly ISpecializationService _service = service;

    /// <summary>Returns all specializations.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SpecializationResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in SpecializationController");
        try
        {
            var specializations = await _service.GetAllSpecializationsAsync();
            return Ok(specializations);
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
            var specialization = await _service.GetSpecializationAsync(id);
            if (specialization is null)
            {
                return NotFound();
            }
            return Ok(specialization);
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
            var entity = await _service.CreateSpecializationAsync(specializationDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
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
            var updated = await _service.UpdateSpecializationAsync(id, specializationDto);
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
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
