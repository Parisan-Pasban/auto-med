using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class UserAccountController(IUserAcountRepository userAccountRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> RegisterUser(AppUser userInput, CancellationToken cancellationToken)
    {
        if (userInput.Information.Password != userInput.Information.ConfirmPassword)
        {
            return BadRequest("Your passwords do not match!");
        }

        LoggedInDto? loggedInDto = await userAccountRepository.RegisterUserAsync(userInput, cancellationToken);

        if (loggedInDto is null)
        {
            return BadRequest("This NationalCode is already exist!");
        }

        return loggedInDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> LoginUser(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? loggedInDto = await userAccountRepository.LoginUserAsync(userInput, cancellationToken);

        if (loggedInDto is null)
        {
            return Unauthorized("FirstName or LastName or Password is wrong");
        }

        return loggedInDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> DeleteUserById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? Result = await userAccountRepository.DeleteUserByIdAsync(userId, cancellationToken);

        if (Result is null)
        {
            return NotFound("User not found");
        }

        return Result;
    }
}