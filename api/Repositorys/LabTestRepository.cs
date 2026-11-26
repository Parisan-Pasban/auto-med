using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class LabTestRepository : ILabTestRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<LabTest> _collection;

    // constructor - dependency injections
    public LabTestRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<LabTest>("Tests");
    }

    #endregion
    public async Task<LabTestDto?> CreateTestAsync(LabTest userInput, CancellationToken cancellationToken)
    {
        LabTest labTest = await _collection.Find(doc => doc.LaboratoryId == userInput.LaboratoryId).FirstOrDefaultAsync(cancellationToken);

        if (labTest is null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        LabTestDto labTestDto = Mappers.ConvertLabTestToLabTestDto(labTest);

        return labTestDto;
    }


    public async Task<List<LabTest>?> GetAllTestAsync(CancellationToken cancellationToken)
    {
        List<LabTest> labTests = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (labTests.Count == 0)
        {
            return null;
        }

        return labTests;
    }

    public async Task<LabTestDto?> GetByNameAsync(string userInput, CancellationToken cancellationToken)
    {
        LabTest labTest = await _collection.Find(doc => doc.Name == userInput).FirstOrDefaultAsync(cancellationToken);

        if (labTest is null)
        {
            return null;
        }

        LabTestDto labTestDto = Mappers.ConvertLabTestToLabTestDto(labTest);

        return labTestDto;
    }

    public async Task<LabTestDto?> UpdateByIdAsync(string userId, LabTest userInput, CancellationToken cancellationToken)
    {
        LabTest labTest = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (labTest is null)
        {
            return null;
        }

        if (labTest.Name == userInput.Name && labTest.Description == userInput.Description && labTest.Price == userInput.Price)
        {
            return null;
        }

        UpdateDefinition<LabTest> updateDef = Builders<LabTest>.Update.Set(doc => doc.Name, userInput.Name)
        .Set(doc => doc.Description, userInput.Description)
        .Set(doc => doc.Price, userInput.Price);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        LabTest UpdateLabTest = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (UpdateLabTest is null)
        {
            return null;
        }

        LabTestDto labTestDto = Mappers.ConvertLabTestToLabTestDto(UpdateLabTest);

        return labTestDto;
    }
    public async Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken)
    {
        LabTest labTest = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (labTest is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}