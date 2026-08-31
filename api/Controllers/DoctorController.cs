using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class DoctorController(IDoctorRepository doctorRepository) : BaseApiController
{
    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<DoctorInformationDto>> UpdateDoctorById(string userId, AppDoctor userInput, CancellationToken cancellationToken)
    {
        DoctorInformationDto? information = await doctorRepository.UpdateDoctorByIdAsync(userId, userInput, cancellationToken);

        if (information is null)
        {
            return BadRequest("Operation Failed!");
        }

        return information;
    }
}