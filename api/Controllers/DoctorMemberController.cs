using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class DoctorMemberController(IDoctorMemberRepository doctorMemberRepository) : BaseApiController
{
    [HttpGet("get-all")]
    public async Task<ActionResult<List<DoctorInformationDto>>> GetAllDoctor(CancellationToken cancellationToken)
    {
        List<AppDoctor>? appDoctors = await doctorMemberRepository.GetAllDoctorAsync(cancellationToken);

        if (appDoctors is null)
        {
            return NotFound();
        }

        List<DoctorInformationDto> doctorInformationDtos = [];

        foreach (AppDoctor doctor in appDoctors)
        {
            DoctorInformationDto doctorInformationDto = new DoctorInformationDto(
                FirstName: doctor.Information.FirstName,
                LastName: doctor.Information.LastName,
                Gender: doctor.Information.Gender,
                PhoneNumber: doctor.Information.PhoneNumber,
                Specialty: doctor.Specialty,
                ClinicName: doctor.ClinicName,
                ClinicNumber: doctor.ClinicNumber,

                ClinicAddress: new AddressDto
                (
                     City: doctor.ClinicAddress.City,
                    Street: doctor.ClinicAddress.Street,
                    Alley: doctor.ClinicAddress.Alley,
                    Plaque: doctor.ClinicAddress.Plaque,
                    Unit: doctor.ClinicAddress.Unit
                )
            );

            doctorInformationDtos.Add(doctorInformationDto);
        }

        return doctorInformationDtos;
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