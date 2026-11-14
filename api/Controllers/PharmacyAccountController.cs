using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class PharmacyAccountController(IPharmacyAccountRepository pharmacyAccountRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<PharmacyLoggedInDto>> RegisterPharmacy(Pharmacy userInput, CancellationToken cancellationToken)
    {
        if (userInput.Password != userInput.ConfirmPassword)
        {
            return BadRequest("Your passwords do not match!");
        }

        PharmacyLoggedInDto? pharmacyLoggedInDto = await pharmacyAccountRepository.RegisterPharmacyAsync(userInput, cancellationToken);

        if (pharmacyLoggedInDto is null)
        {
            return BadRequest("This LicenseNumber is already exist!");
        }

        return pharmacyLoggedInDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<PharmacyLoggedInDto>> LoginPharmacy(PharmacyLoginDtp userInput, CancellationToken cancellationToken)
    {
        PharmacyLoggedInDto? pharmacyLoggedInDto = await pharmacyAccountRepository.LoginPharmacyAsync(userInput, cancellationToken);

        if (pharmacyLoggedInDto is null)
        {
            return Unauthorized("Name or LicenseNumber or Password is wrong");
        }

        return pharmacyLoggedInDto;
    }


    [HttpGet("get-all")]
    public async Task<ActionResult<List<PharmacyMemberDto>>> GetAllPharmacy(CancellationToken cancellationToken)
    {
        List<Pharmacy>? pharmacies = await pharmacyAccountRepository.GetAllPharmacyAsync(cancellationToken);

        if (pharmacies is null)
        {
            return NoContent();
        }

        List<PharmacyMemberDto> pharmacyMedicineDtos = [];

        foreach (Pharmacy pharmacy in pharmacies)
        {
            PharmacyMemberDto pharmacyMemberDto = Mappers.ConvertPharmacyToPharmacyMemberDto(pharmacy);

            pharmacyMedicineDtos.Add(pharmacyMemberDto);
        }

        return pharmacyMedicineDtos;
    }

    [HttpGet("get-by-City/{userInput}")]
    public async Task<ActionResult<PharmacyMemberDto>> GetByCity(string userInput, CancellationToken cancellationToken)
    {
        PharmacyMemberDto? pharmacyMemberDto = await pharmacyAccountRepository.GetByCityAsync(userInput, cancellationToken);

        if (pharmacyMemberDto is null)
        {
            return NotFound("pharmacy not found!");
        }

        return pharmacyMemberDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<PharmacyMemberDto>> UpdatePharmacyById(string userId, Pharmacy userInput, CancellationToken cancellationToken)
    {
        PharmacyMemberDto? pharmacyMemberDto = await pharmacyAccountRepository.UpdatePharmacyByIdAsync(userId, userInput, cancellationToken);

        if (pharmacyMemberDto is null)
        {
            return BadRequest("Operation Failed!");

        }

        return pharmacyMemberDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> DeletePharmacyById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? Result = await pharmacyAccountRepository.DeletePharmacyByIdAsync(userId, cancellationToken);

        if (Result is null)
        {
            return NotFound("User not found");

        }

        return Result;
    }
}