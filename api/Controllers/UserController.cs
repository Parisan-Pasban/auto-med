using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UserController(IUserRepository userRepository) : BaseApiController
{
    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<MemberDto>> UpdateUserById(string userId, AppUser userInput, CancellationToken cancellationToken)
    {
        MemberDto? memberDto = await userRepository.UpdateUserByIdAsync(userId, userInput, cancellationToken);

        if (memberDto is null)
        {
            return BadRequest("Operation Failed!");
        }

        return memberDto;
    }
}