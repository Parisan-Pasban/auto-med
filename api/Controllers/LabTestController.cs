using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class LabTestController(ILabTestRepository labTestRepository) : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<LabTestDto>> CreateTest(LabTest userInput, CancellationToken cancellationToken)
    {
        LabTestDto? labTestDto = await labTestRepository.CreateTestAsync(userInput, cancellationToken);

        if (labTestDto is null)
        {
            return BadRequest("");
        }

        return labTestDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<LabTestDto>>> GetAllTest(CancellationToken cancellationToken)
    {
        List<LabTest>? labTests = await labTestRepository.GetAllTestAsync(cancellationToken);

        if (labTests is null)
        {
            return NotFound();
        }

        List<LabTestDto> labTestDtos = [];

        foreach (LabTest labTest in labTests)
        {
            LabTestDto labTestDto = Mappers.ConvertLabTestToLabTestDto(labTest);

            labTestDtos.Add(labTestDto);
        }

        return labTestDtos;
    }

    [HttpGet("get-by-name/{userInput}")]
    public async Task<ActionResult<LabTestDto>> GetByName(string userInput, CancellationToken cancellationToken)
    {
        LabTestDto? labTestDto = await labTestRepository.GetByNameAsync(userInput, cancellationToken);

        if (labTestDto is null)
        {
            return NotFound("name not found!");
        }

        return labTestDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<LabTestDto>> UpdateById(string userId, LabTest userInput, CancellationToken cancellationToken)
    {
        LabTestDto? labTestDto = await labTestRepository.UpdateByIdAsync(userId, userInput, cancellationToken);
        if (labTestDto is null)
        {
            return BadRequest("Operation Failed!");

        }

        return labTestDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> DeleteById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? result = await labTestRepository.DeleteByIdAsync(userId, cancellationToken);

        if(result is null)
        {
            return NotFound("Test not found!");
        }

        return result;
    }
}