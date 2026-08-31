using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class DoctorAccountController(IDoctorAccountRepository doctorAccountRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> RegisterDoctor(AppDoctor userInput, CancellationToken cancellationToken)
    {
        if (userInput.Information.Password != userInput.Information.ConfirmPassword)
        {
            return BadRequest("Your passwords do not match!");
        }

        LoggedInDto? loggedInDto = await doctorAccountRepository.RegisterDoctorAsync(userInput, cancellationToken);

        if (loggedInDto is null)
        {
            return BadRequest("This NationalCode is already exist!");
        }

        return loggedInDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> LoginDoctor(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? loggedInDto = await doctorAccountRepository.LoginDoctorAsync(userInput, cancellationToken);

        if (loggedInDto is null)
        {
            return Unauthorized("FirstName or LastName or Password is wrong");

        }

        return loggedInDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> DeleteDoctorById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? Result = await doctorAccountRepository.DeleteUserByIdAsync(userId, cancellationToken);

        if (Result is null)
        {
            return NotFound("User not found");
        }

        return Result;
    }
}