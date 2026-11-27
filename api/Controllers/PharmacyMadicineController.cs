using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class PharmacyMadicineController(IPharmacyMadicineRepository pharmacyMadicineRepository) : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<PharmacyMadicineDto>> CreatePharmacyMadicine(PharmacyMadicine userInput, CancellationToken cancellationToken)
    {
        PharmacyMadicineDto? pharmacyMadicineDto = await pharmacyMadicineRepository.CreatePharmacyMadicineAsync(userInput, cancellationToken);

        if (pharmacyMadicineDto is null)
        {
            return BadRequest("");
        }

        return pharmacyMadicineDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<PharmacyMadicineDto>>> GetAllPharmacyMedicine(CancellationToken cancellationToken)
    {
        List<PharmacyMadicine>? pharmacyMadicines = await pharmacyMadicineRepository.GetAllPharmacyMedicineAsync(cancellationToken);

        if (pharmacyMadicines is null)
        {
            return NotFound();
        }

        List<PharmacyMadicineDto> pharmacyMadicineDtos = [];

        foreach (PharmacyMadicine pharmacyMadicine in pharmacyMadicines)
        {
            PharmacyMadicineDto pharmacyMadicineDto = Mappers.ConvertPharmacyMedicineToPharmacyMedicineDto(pharmacyMadicine);

            pharmacyMadicineDtos.Add(pharmacyMadicineDto);
        }

        return pharmacyMadicineDtos;
    }

    [HttpGet("get-by-PharmacyId/{PharmacyId}")]
    public async Task<ActionResult<PharmacyMadicineDto>> GetByPharmacyId(string PharmacyId, CancellationToken cancellationToken)
    {
        PharmacyMadicineDto? pharmacyMadicineDto = await pharmacyMadicineRepository.GetByPharmacyIdAsync(PharmacyId, cancellationToken);

        if (pharmacyMadicineDto is null)
        {
            return NotFound("pharmacy Id not found!");
        }

        return pharmacyMadicineDto;
    }

    [HttpGet("get-by-MedicineId/{MedicineId}")]
    public async Task<ActionResult<PharmacyMadicineDto>> GetByMedicineId(string MedicineId, CancellationToken cancellationToken)
    {
        PharmacyMadicineDto? pharmacyMadicineDto = await pharmacyMadicineRepository.GetByMedicineIdAsync(MedicineId, cancellationToken);

        if (pharmacyMadicineDto is null)
        {
            return NotFound("medicine Id not found!");
        }

        return pharmacyMadicineDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<PharmacyMadicineDto>> UpdateById(string userId, PharmacyMadicine userInput, CancellationToken cancellationToken)
    {
        PharmacyMadicineDto? pharmacyMadicineDto = await pharmacyMadicineRepository.UpdateByIdAsync(userId, userInput, cancellationToken);

        if (pharmacyMadicineDto is null)
        {
            return BadRequest("Operation Failed!");
        }

        return pharmacyMadicineDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> DeleteById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? result = await pharmacyMadicineRepository.DeleteByIdAsync(userId, cancellationToken);

        if (result is null)
        {
            return NotFound("pharmacy medicine not found!");
        }

        return result;
    }
}