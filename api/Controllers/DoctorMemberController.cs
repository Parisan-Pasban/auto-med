using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class DoctorMemberController(IDoctorMemberRepository doctorMemberRepository) : BaseApiController
{
    [HttpGet("get-all")]
    public async Task<ActionResult<List<LoggedInDto>>> GetAllDoctor(CancellationToken cancellationToken)
    {
        List<AppDoctor>? appDoctors = await doctorMemberRepository.GetAllDoctorAsync(cancellationToken);

        if (appDoctors is null)
        {
            return NotFound();
        }

        List<LoggedInDto> loggedInDtos = [];

        foreach (AppDoctor doctor in appDoctors)
        {
            LoggedInDto loggedInDto = new LoggedInDto(
                FirstName: doctor.Information.FirstName,
                LastName: doctor.Information.LastName
            );

            loggedInDtos.Add(loggedInDto);
        }

        return loggedInDtos;
    }

    [HttpGet("get-by-lastname/{userInput}")]
    public async Task<ActionResult<DoctorInformationDto>> GetByLastName(string userInput, CancellationToken cancellationToken)
    {
        DoctorInformationDto? informationDto = await doctorMemberRepository.GetByLastNameAsync(userInput, cancellationToken);

        if (informationDto is null)
        {
            return NotFound("doctor not found!");
        }

        return informationDto;
    }

    [HttpGet("get-by-specialty/{userInput}")]
    public async Task<ActionResult<DoctorInformationDto>> GetBySpecialty(string userInput, CancellationToken cancellationToken)
    {
        DoctorInformationDto? informationDto = await doctorMemberRepository.GetBySpecialtyAsync(userInput, cancellationToken);

        if (informationDto is null)
        {
            return NotFound("doctor not found!");
        }

        return informationDto;
    }
}