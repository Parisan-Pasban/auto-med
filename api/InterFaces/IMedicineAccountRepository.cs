using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface IMedicineAccountRepository
{
    public Task<MedicineInformationDto?> RegisterMedicineAsync(Medicine userInput, CancellationToken cancellationToken);

    public Task<List<Medicine>?> GetAllMedicineAsync(CancellationToken cancellationToken);

    public Task<MedicineInformationDto?> GetByGenericNameAsync(string userInput, CancellationToken cancellationToken);

    public Task<MedicineInformationDto?> GetByGenericFormAsync(string userInput, CancellationToken cancellationToken);

    public Task<MedicineInformationDto?> UpdateMedicineByIdAsync(string userId, Medicine userInput, CancellationToken cancellationToken);

    public Task<DeleteResult?> deleteMedicineByIdAsync(string userId, CancellationToken cancellationToken);

}