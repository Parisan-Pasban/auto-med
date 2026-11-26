using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface ILabTestRepository
{
    public Task<LabTestDto?> CreateTestAsync(LabTest userInput, CancellationToken cancellationToken);
    public Task<List<LabTest>?> GetAllTestAsync(CancellationToken cancellationToken);
    public Task<LabTestDto?> GetByNameAsync(string userInput, CancellationToken cancellationToken);
    public Task<LabTestDto?> UpdateByIdAsync(string userId, LabTest userInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken);
}