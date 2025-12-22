using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationsController(ILogger<SpecializationsController> logger, ISpecializationService service) : ControllerBase
{
    /// <summary>Returns all specializations.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SpecializationResponse>>> GetAll()
    {
        logger.LogInformation("Called GetAll in SpecializationController");
        try
        {
            var specializations = await service.GetAllSpecializationsAsync();
            return Ok(specializations);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetAll");
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
        logger.LogInformation("Called GetById in SpecializationController");
        try
        {
            var specialization = await service.GetSpecializationAsync(id);
            if (specialization is null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetById");
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
        logger.LogInformation("Called Create in SpecializationController");

        if (specializationDto is null)
        {
            return BadRequest("Specialization data is required.");
        }

        try
        {
            var entity = await service.CreateSpecializationAsync(specializationDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Create");
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
        logger.LogInformation("Called Update in SpecializationController");

        if (specializationDto is null)
        {
            return BadRequest("Specialization data is required.");
        }

        try
        {
            var updated = await service.UpdateSpecializationAsync(id, specializationDto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Update");
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
        logger.LogInformation("Called Delete in SpecializationController");

        try
        {
            var deleted = await service.DeleteSpecializationAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
