using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController(ILogger<PatientsController> logger, IPatientService service) : ControllerBase
{
    /// <summary>Returns all patients.</summary>
    /// <returns>A list of all patients in the system.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PatientResponse>>> GetAll()
    {
        logger.LogInformation("Called GetAll in PatientController");
        try
        {
            var patients = await service.GetAllPatientsAsync();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a patient by their unique identifier.</summary>
    /// <param name="id">The unique identifier of the patient to retrieve.</param>
    /// <returns>The patient with the specified identifier, if found.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientResponse>> GetById(Guid id)
    {
        logger.LogInformation("Called GetById in PatientController");
        try
        {
            var patient = await service.GetPatientAsync(id);
            if (patient is null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new patient.</summary>
    /// <param name="PatientDto">The patient data to create.</param>
    /// <returns>The newly created patient with its assigned identifier.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] PatientRequest PatientDto)
    {
        logger.LogInformation("Called Create in PatientController");

        if (PatientDto is null)
        {
            return BadRequest("Patient data is required.");
        }

        try
        {
            var entity = await service.CreatePatientAsync(PatientDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates an existing patient by their identifier.</summary>
    /// <param name="id">The unique identifier of the patient to update.</param>
    /// <param name="PatientDto">The updated patient data.</param>
    /// <returns>The updated patient, if found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientResponse>> Update(Guid id, [FromBody] PatientRequest PatientDto)
    {
        logger.LogInformation("Called Update in PatientController");

        if (PatientDto is null)
        {
            return BadRequest("Patient data is required.");
        }

        try
        {
            var updated = await service.UpdatePatientAsync(id, PatientDto);
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

    /// <summary>Deletes a patient by their identifier.</summary>
    /// <param name="id">The unique identifier of the patient to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("Called Delete in PatientController");

        try
        {
            var deleted = await service.DeletePatientAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
