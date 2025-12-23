using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController(ILogger<DoctorsController> logger, IDoctorService service) : ControllerBase
{
    /// <summary>Returns all doctors.</summary>
    /// <returns>A list of all doctors in the system.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<DoctorResponse>>> GetAll()
    {
        logger.LogInformation("Called GetAll in DoctorController");
        try
        {
            var doctors = await service.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a doctor by their unique identifier.</summary>
    /// <param name="id">The unique identifier of the doctor to retrieve.</param>
    /// <returns>The doctor with the specified identifier, if found.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorResponse>> GetById(Guid id)
    {
        logger.LogInformation("Called GetById in DoctorController");
        try
        {
            var doctor = await service.GetDoctorAsync(id);
            if (doctor is null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new doctor.</summary>
    /// <param name="doctorDto">The doctor data to create.</param>
    /// <returns>The newly created doctor with its assigned identifier.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] DoctorRequest doctorDto)
    {
        logger.LogInformation("Called Create in DoctorController");

        if (doctorDto is null)
        {
            return BadRequest("Doctor data is required.");
        }

        try
        {
            var entity = await service.CreateDoctorAsync(doctorDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates an existing doctor by their identifier.</summary>
    /// <param name="id">The unique identifier of the doctor to update.</param>
    /// <param name="doctorDto">The updated doctor data.</param>
    /// <returns>The updated doctor, if found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DoctorResponse>> Update(Guid id, [FromBody] DoctorRequest doctorDto)
    {
        logger.LogInformation("Called Update in DoctorController");

        if (doctorDto is null)
        {
            return BadRequest("Doctor data is required.");
        }

        try
        {
            var updated = await service.UpdateDoctorAsync(id, doctorDto);
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

    /// <summary>Deletes a doctor by their identifier.</summary>
    /// <param name="id">The unique identifier of the doctor to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("Called Delete in DoctorController");

        try
        {
            var deleted = await service.DeleteDoctorAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
