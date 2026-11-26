using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface ILaboratoryRepository
{
    public Task<LaboratoryDto?> CreateLaboratoryAsync(Laboratory userInput, CancellationToken cancellationToken);
    public Task<LaboratoryDto?> LoginLaboratoryAsync(LaboratoryLoginDto userInput, CancellationToken cancellationToken);
    public Task<List<Laboratory>?> GetAllLaboratoryAsync(CancellationToken cancellationToken1);
    public Task<LaboratoryDto?> GetByCityAsync(string userInput, CancellationToken cancellationToken);
    public Task<LaboratoryDto?> UpdateByIdAsync(string userId, Laboratory userInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken);
}