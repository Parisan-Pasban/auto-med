using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface IPharmacyMadicineRepository
{
    public Task<PharmacyMadicineDto?> CreatePharmacyMadicineAsync(PharmacyMadicine userInput, CancellationToken cancellationToken);
    public Task<List<PharmacyMadicine>?> GetAllPharmacyMedicineAsync(CancellationToken cancellationToken);
    public Task<PharmacyMadicineDto?> GetByPharmacyIdAsync(string PharmacyId, CancellationToken cancellationToken);
    public Task<PharmacyMadicineDto?> GetByMedicineIdAsync(string MedicineId, CancellationToken cancellationToken);
    public Task<PharmacyMadicineDto?> UpdateByIdAsync(string userId, PharmacyMadicine userInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken);
}