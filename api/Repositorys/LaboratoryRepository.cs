using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class LaboratoryRepository : ILaboratoryRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<Laboratory> _collection;

    public LaboratoryRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Laboratory>("Laboratories");
    }


    #endregion
    public async Task<LaboratoryDto?> CreateLaboratory(Laboratory userInput, CancellationToken cancellationToken)
    {
        Laboratory laboratory = await _collection.Find(doc => doc.LicenseNumber == userInput.LicenseNumber).FirstOrDefaultAsync(cancellationToken);

        if (laboratory is null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        LaboratoryDto laboratoryDto = Mappers.ConvertLaboratoryToLaboratoryDto(userInput);

        return laboratoryDto;
    }

    public async Task<LaboratoryDto?> LoginLaboratory(LaboratoryLoginDto userInput, CancellationToken cancellationToken)
    {
        Laboratory laboratory = await _collection.Find(doc => doc.Name == userInput.Name && doc.LicenseNumber == userInput.LicenseNumber && doc.Password == userInput.Password).FirstOrDefaultAsync(cancellationToken);

        if (laboratory is null)
        {
            return null;
        }

        LaboratoryDto laboratoryDto = Mappers.ConvertLaboratoryToLaboratoryDto(laboratory);

        return laboratoryDto;
    }

    public async Task<List<Laboratory>?> GetAllLaboratory(CancellationToken cancellationToken)
    {
        List<Laboratory> laboratories = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (laboratories.Count == 0)
        {
            return null;
        }

        return laboratories;
    }

    public async Task<LaboratoryDto?> GetByCity(string userInput, CancellationToken cancellationToken)
    {
        Laboratory laboratory = await _collection.Find(doc => doc.AddressLaboratory.City == userInput).FirstOrDefaultAsync(cancellationToken);

        if (laboratory is null)
        {
            return null;
        }

        LaboratoryDto laboratoryDto = Mappers.ConvertLaboratoryToLaboratoryDto(laboratory);

        return laboratoryDto;
    }

    public async Task<LaboratoryDto?> UpdateById(string userId, Laboratory userInput, CancellationToken cancellationToken)
    {
        Laboratory laboratory = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (laboratory is null)
        {
            return null;
        }

        if (laboratory.Name == userInput.Name && laboratory.PhoneNumber == userInput.PhoneNumber)
        {
            return null;
        }

        UpdateDefinition<Laboratory> updateDef = Builders<Laboratory>.Update.Set(doc => doc.Name, userInput.Name)
        .Set(doc => doc.PhoneNumber, userInput.PhoneNumber);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        Laboratory UpdateLaboratory = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (UpdateLaboratory is null)
        {
            return null;
        }

        LaboratoryDto laboratoryDto = Mappers.ConvertLaboratoryToLaboratoryDto(laboratory);

        return laboratoryDto;
    }
    public async Task<DeleteResult?> DeleteById(string userId, CancellationToken cancellationToken)
    {
        Laboratory laboratory = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (laboratory is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}