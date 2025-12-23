using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService service) : ControllerBase
{    
    /// <summary>Returns all appointments.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAll()
    {
        logger.LogInformation("Called GetAll in AppointmentController");
        try
        {
            var appointments = await service.GetAllAppointmentsAsync();            
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetAll");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Returns a Appointment by Id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentResponse>> GetById(Guid id)
    {
        logger.LogInformation("Called GetById in AppointmentController");
        try
        {
            var appointment = await service.GetAppointmentAsync(id);
            if (appointment is null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in GetById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Creates a new Appointment.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] AppointmentRequest AppointmentDto)
    {
        logger.LogInformation("Called Create in AppointmentController");

        if (AppointmentDto is null)
        {
            return BadRequest("Appointment data is required.");
        }

        try
        {
            var entity = await service.CreateAppointmentAsync(AppointmentDto);
            return CreatedAtAction(nameof(GetById), new {entity.Id}, entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Create");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>Updates a Appointment by Id.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentResponse>> Update(Guid id, [FromBody] AppointmentRequest AppointmentDto)
    {
        logger.LogInformation("Called Update in AppointmentController");

        if (AppointmentDto is null)
        {
            return BadRequest("Appointment data is required.");
        }

        try
        {
            var updated = await service.UpdateAppointmentAsync(id, AppointmentDto);
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

    /// <summary>Deletes a Appointment by Id.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        logger.LogInformation("Called Delete in AppointmentController");

        try
        {
            var deleted = await service.DeleteAppointmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred in Delete");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
