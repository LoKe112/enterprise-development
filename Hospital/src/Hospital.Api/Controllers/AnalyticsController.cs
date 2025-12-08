using Hospital.Application.Services.Abstractions;
using Hospital.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Api.Controllers;

/// <summary>
/// Controller for analytical queries and reports.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AnalyticsController"/> class.
/// </remarks>
/// <param name="analyticsService">The analytics service.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    private readonly IAnalyticsService _analyticsService = analyticsService;
    private readonly ILogger<AnalyticsController> _logger = logger;

    /// <summary>
    /// Gets doctors with 10 or more years of experience.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of doctors with at least 10 years of experience.</returns>
    /// <response code="200">Returns the list of experienced doctors.</response>
    [HttpGet("doctors/experienced")]
    [ProducesResponseType(typeof(List<DoctorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DoctorResponse>>> GetExperiencedDoctors(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting doctors with at least 10 years of experience");

        var result = await _analyticsService.GetDoctorsWithExperienceAtLeast10Async(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets patients assigned to a specific doctor, ordered by full name.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of patients for the specified doctor.</returns>
    /// <response code="200">Returns the list of patients.</response>
    /// <response code="400">If the doctor ID is invalid.</response>
    [HttpGet("doctors/{doctorId:guid}/patients")]
    [ProducesResponseType(typeof(List<PatientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PatientResponse>>> GetPatientsByDoctor(
        [FromRoute] Guid doctorId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting patients for doctor with ID: {DoctorId}", doctorId);

        if (doctorId == Guid.Empty)
        {
            return BadRequest("Doctor ID is required.");
        }

        var result = await _analyticsService.GetPatientsByDoctorOrderedByFullNameAsync(
            doctorId, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Gets follow-up appointment counts per patient for the last month.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of patients with their follow-up appointment counts.</returns>
    /// <response code="200">Returns the list of patients with appointment counts.</response>
    [HttpGet("appointments/follow-up-counts")]
    [ProducesResponseType(typeof(List<(PatientResponse, int)>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<(PatientResponse Patient, int Count)>>> GetFollowUpAppointmentCounts(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting follow-up appointment counts for the last month");

        var result = await _analyticsService.GetFollowUpAppointmentsCountLastMonthAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets patients over 30 years old who have appointments with multiple doctors.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of patients over 30 with multiple doctors.</returns>
    /// <response code="200">Returns the list of patients.</response>
    [HttpGet("patients/over-30-multiple-doctors")]
    [ProducesResponseType(typeof(List<PatientResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PatientResponse>>> GetPatientsOver30WithMultipleDoctors(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting patients over 30 with appointments to multiple doctors");

        var result = await _analyticsService.GetPatientsOver30WithMultipleDoctorsOrderedByBirthDateAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets appointments in a specific room for the current month.
    /// </summary>
    /// <param name="roomNumber">The room number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of appointments in the specified room.</returns>
    /// <response code="200">Returns the list of appointments.</response>
    /// <response code="400">If the room number is empty or null.</response>
    [HttpGet("appointments/room/{roomNumber}")]
    [ProducesResponseType(typeof(List<AppointmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAppointmentsInRoom(
        [FromRoute][Required] string roomNumber,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting appointments in room: {RoomNumber} for current month", roomNumber);

        if (string.IsNullOrWhiteSpace(roomNumber))
        {
            return BadRequest("Room number is required.");
        }

        var result = await _analyticsService.GetAppointmentsInSelectedRoomThisMonthAsync(
            roomNumber, cancellationToken);

        return Ok(result);
    }
}