using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class AppointmentController(IAppointmentRepository appointmentRepository) : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment(Appointment userInput, CancellationToken cancellationToken)
    {
        AppointmentDto? appointmentDto = await appointmentRepository.CreateAppointment(userInput, cancellationToken);

        if (appointmentDto is null)
        {
            return BadRequest("This doctor ID does not exist.");
        }

        return appointmentDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<AppointmentDto>>> GetAllAppointment(CancellationToken cancellationToken)
    {
        List<Appointment>? appointments = await appointmentRepository.GetAllAppointment(cancellationToken);

        if (appointments is null)
        {
            return NotFound();
        }

        List<AppointmentDto> appointmentDtos = [];

        foreach (Appointment appointment in appointments)
        {
            AppointmentDto appointmentDto = new AppointmentDto(
                Date: appointment.Date,
                StartTime: appointment.StartTime,
                EndTime: appointment.EndTime,
                AppointmentDuration: appointment.AppointmentDuration,
                BreakDuration: appointment.BreakDuration,
                MaxPatients: appointment.MaxPatients
            );

            appointmentDtos.Add(appointmentDto);
        }

        return appointmentDtos;
    }

    [HttpGet("get-by-Date/{userInput}")]
    public async Task<ActionResult<AppointmentDto>> GetByDate(DateTime userInput, CancellationToken cancellationToken)
    {

        AppointmentDto? appointmentDto = await appointmentRepository.GetByDate(userInput, cancellationToken);

        if (appointmentDto is null)
        {
            return NotFound("date not found!");
        }

        return appointmentDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointmentById(string userId, Appointment userInput, CancellationToken cancellationToken)
    {
        AppointmentDto? appointmentDto = await appointmentRepository.UpdateAppointmentById(userId, userInput, cancellationToken);

        if (appointmentDto is null)
        {
            return BadRequest("Operation Failed!");
        }

        return appointmentDto;
    }

    [HttpDelete("delete-by-id/{userid}")]
    public async Task<ActionResult<DeleteResult>> DeleteById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? Result = await appointmentRepository.DeleteById(userId, cancellationToken);

        if(Result is null)
        {
            return NotFound("Appointment not found");
        }

        return Result;
    }

}