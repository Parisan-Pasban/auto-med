using System.Drawing;
using api.Controllers.Helper;
using api.DTOs;
using api.InterFaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

public class MedicineAccountController(IMedicineAccountRepository medicineAccountRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<MedicineInformationDto>> RegisterMedicine(Medicine userInput, CancellationToken cancellationToken)
    {
        MedicineInformationDto? medicineInformationDto = await medicineAccountRepository.RegisterMedicineAsync(userInput, cancellationToken);

        if (medicineInformationDto is null)
        {
            return BadRequest("This LicenseNumber is already exist!");

        }

        return medicineInformationDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<List<MedicineInformationDto>>> GetAllMedicine(CancellationToken cancellationToken)
    {
        List<Medicine>? medicines = await medicineAccountRepository.GetAllMedicineAsync(cancellationToken);

        if (medicines is null)
        {
            return NotFound();
        }

        List<MedicineInformationDto> medicineInformationDtos = [];

        foreach (Medicine medicine in medicines)
        {
        MedicineInformationDto medicineInformationDto = new MedicineInformationDto(
            GenericName: medicine.GenericName,
            BrandName: medicine.BrandName,
            Form: medicine.Form,
            Dosage: medicine.Dosage,
            ManuFacturer: medicine.ManuFacturer,
            CountryOfOrigin: medicine.CountryOfOrigin,
            RequiresPrescription: medicine.RequiresPrescription,
            PharmacyMedicine: new PharmacyMedicineDto(
                Price: medicine.PharmacyMadicine.Price,
                Inventory: medicine.PharmacyMadicine.Inventory
            )
        );

            medicineInformationDtos.Add(medicineInformationDto);
        }

        return medicineInformationDtos;
    }

    [HttpGet("get-by-genericname/{userInput}")]
    public async Task<ActionResult<MedicineInformationDto>> GetByGenericName(string userInput, CancellationToken cancellationToken)
    {
        MedicineInformationDto? medicineInformationDto = await medicineAccountRepository.GetByGenericNameAsync(userInput, cancellationToken);

        if (medicineInformationDto is null)
        {
            return NotFound("medicine not found!");
        }

        return medicineInformationDto;
    }

    [HttpGet("get-by-form/{userInput}")]
    public async Task<ActionResult<MedicineInformationDto>> GetByGenericForm(string userInput, CancellationToken cancellationToken)
    {
        MedicineInformationDto? medicineInformationDto = await medicineAccountRepository.GetByGenericFormAsync(userInput, cancellationToken);

        if (medicineInformationDto is null)
        {
            return NotFound("medicine not found!");
        }

        return medicineInformationDto;
    }

    [HttpPut("update-by-id/{userId}")]
    public async Task<ActionResult<MedicineInformationDto>> UpdateMedicineById(string userId, Medicine userInput, CancellationToken cancellationToken)
    {
        MedicineInformationDto? medicineInformationDto = await medicineAccountRepository.UpdateMedicineByIdAsync(userId, userInput, cancellationToken);

        if (medicineInformationDto is null)
        {
            return BadRequest("Operation Failed!");
        }

        return medicineInformationDto;
    }

    [HttpDelete("delete-by-id/{userId}")]
    public async Task<ActionResult<DeleteResult>> deleteMedicineById(string userId, CancellationToken cancellationToken)
    {
        DeleteResult? Result = await medicineAccountRepository.deleteMedicineByIdAsync(userId, cancellationToken);

        if (Result is null)
        {
            return NotFound("Medicine not found");
        }

        return Result;
    }
}