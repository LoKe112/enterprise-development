using Hospital.Api.Mappers;
using Hospital.Contracts;
using Hospital.Domain.Models;
using Hospital.Domain.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;
namespace Hospital.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly ILogger<AppointmentsController> _logger;
    private readonly IAppointmentService _service;

    public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>Returns all appointments.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAll()
    {
        _logger.LogInformation("Called GetAll in AppointmentController");
        try
        {
            List<Appointment> appointments = await _service.GetAllAppointmentsAsync();
            var response = appointments.Select(s => s.ToResponse()).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll");
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
        _logger.LogInformation("Called GetById in AppointmentController");
        try
        {
            Appointment? appointment = await _service.GetAppointmentAsync(id);
            if (appointment is null)
            {
                return NotFound();
            }
            return Ok(appointment.ToResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetById");
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
        _logger.LogInformation("Called Create in AppointmentController");

        if (AppointmentDto is null)
        {
            return BadRequest("Appointment data is required.");
        }

        try
        {
            Appointment entity = await _service.CreateAppointmentAsync(AppointmentDto.ToDomain());
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

    /// <summary>Updates a Appointment by Id.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentResponse>> Update(Guid id, [FromBody] AppointmentRequest AppointmentDto)
    {
        _logger.LogInformation("Called Update in AppointmentController");

        if (AppointmentDto is null)
        {
            return BadRequest("Appointment data is required.");
        }

        try
        {
            Appointment appointmentToUpdate = AppointmentDto.ToDomain();

            Appointment? updated = await _service.UpdateAppointmentAsync(id, appointmentToUpdate);
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

    /// <summary>Deletes a Appointment by Id.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Called Delete in AppointmentController");

        try
        {
            var deleted = await _service.DeleteAppointmentAsync(id);
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
