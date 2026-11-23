using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface ILaboratoryRepository
{
    public Task<LaboratoryDto?> CreateLaboratory(Laboratory userInput, CancellationToken cancellationToken);
    public Task<LaboratoryDto?> LoginLaboratory(LaboratoryLoginDto userInput, CancellationToken cancellationToken);
    public Task<List<Laboratory>?> GetAllLaboratory(CancellationToken cancellationToken1);
    public Task<LaboratoryDto?> GetByCity(string userInput, CancellationToken cancellationToken);
    public Task<LaboratoryDto?> UpdateById(string userId, Laboratory userInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteById(string userId, CancellationToken cancellationToken);
}