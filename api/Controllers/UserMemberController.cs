using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UserMemberController(IUserMemberRepository userMemberRepository) : BaseApiController
{
    [HttpGet("get-all")]
    public async Task<ActionResult<List<MemberDto>>> GetAllUser(CancellationToken cancellationToken)
    {
        List<AppUser>? appUsers = await userMemberRepository.GetAllUserAsync(cancellationToken);

        if (appUsers is null)
        {
            return NoContent();
        }

        List<MemberDto> memberDtos = [];

        foreach (AppUser user in appUsers)
        {
            MemberDto memberDto = Mappers.ConvertAppUserToMemberDto(user);

            memberDtos.Add(memberDto);
        }

        return memberDtos;
    }

    [HttpGet("get-by-lastname/{userInput}")]
    public async Task<ActionResult<MemberDto>> GetByLastName(string userInput, CancellationToken cancellationToken)
    {
        MemberDto? memberDto = await userMemberRepository.GetByLastNameAsync(userInput, cancellationToken);

        if (memberDto is null)
        {
            return NotFound("User not found!");
        }

        return memberDto;
    }
}