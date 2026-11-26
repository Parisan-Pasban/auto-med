using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class LaboratoryController(ILaboratoryRepository laboratoryRepository) : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<LaboratoryDto>> CreateLaboratory(Laboratory userInput, CancellationToken cancellationToken)
    {
        LaboratoryDto? laboratoryDto = await laboratoryRepository.CreateLaboratoryAsync(userInput, cancellationToken);

        if (laboratoryDto is null)
        {
            return BadRequest("");
        }

        return laboratoryDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LaboratoryDto>> LoginLaboratory(LaboratoryLoginDto userInput, CancellationToken cancellationToken)
    {
        LaboratoryDto? laboratoryDto = await laboratoryRepository.LoginLaboratoryAsync(userInput, cancellationToken);

        if (laboratoryDto is null)
        {
            return Unauthorized("Name or LicenseNumber or Password is wrong");
        }

        return laboratoryDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<LaboratoryDto>>> GetAllLaboratory(CancellationToken cancellationToken)
    {
        List<Laboratory>? laboratories = await laboratoryRepository.GetAllLaboratoryAsync(cancellationToken);

        if (laboratories is null)
        {
            return NotFound();
        }

        List<LaboratoryDto> laboratoryDtos = [];

        foreach (Laboratory laboratory in laboratories)
        {
            LaboratoryDto laboratoryDto = Mappers.ConvertLaboratoryToLaboratoryDto(laboratory);

            laboratoryDtos.Add(laboratoryDto);
        }

        return laboratoryDtos;
    }

    [HttpGet("get-by-city/(userInput)")]
    public async Task<ActionResult<LaboratoryDto>> GetByCity(string userInput, CancellationToken cancellationToken)
    {
        LaboratoryDto? laboratoryDto = await laboratoryRepository.GetByCityAsync(userInput, cancellationToken);

        if (laboratoryDto is null)
        {
            return NotFound("city not found!");
        }

        return laboratoryDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<LaboratoryDto>> UpdateById(string userId, Laboratory userInput, CancellationToken cancellationToken)
    {
        LaboratoryDto? laboratoryDto = await laboratoryRepository.UpdateByIdAsync(userId, userInput, cancellationToken);

        if (laboratoryDto is null)
        {
            return BadRequest("Operation Failed!");
        }

        return laboratoryDto;
    }

    [HttpDelete("delete-by-id/(userId)")]
    public async Task<ActionResult<DeleteResult>> DeleteById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? result = await laboratoryRepository.DeleteByIdAsync(userId, cancellationToken);

        if (result is null)
        {
            return NotFound("Laboratory not found");
        }

        return result;
    }
}