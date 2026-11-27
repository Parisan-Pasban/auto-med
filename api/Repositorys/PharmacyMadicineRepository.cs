using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class PharmacyMadicineRepository : IPharmacyMadicineRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<PharmacyMadicine> _collection;

    // constructor - dependency injections
    public PharmacyMadicineRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<PharmacyMadicine>("pharmacyMadicines");
    }
    #endregion

    public async Task<PharmacyMadicineDto?> CreatePharmacyMadicineAsync(PharmacyMadicine userInput, CancellationToken cancellationToken)
    {
        PharmacyMadicine pharmacyMadicine = await _collection.Find(doc => doc.PharmacyId == userInput.PharmacyId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacyMadicine is null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        PharmacyMadicineDto pharmacyMadicineDto = Mappers.ConvertPharmacyMedicineToPharmacyMedicineDto(pharmacyMadicine);

        return pharmacyMadicineDto;
    }

    public async Task<List<PharmacyMadicine>?> GetAllPharmacyMedicineAsync(CancellationToken cancellationToken)
    {
        List<PharmacyMadicine> pharmacyMadicines = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (pharmacyMadicines.Count == 0)
        {
            return null;
        }

        return pharmacyMadicines;
    }

    public async Task<PharmacyMadicineDto?> GetByPharmacyIdAsync(string PharmacyId, CancellationToken cancellationToken)
    {
        PharmacyMadicine pharmacyMadicine = await _collection.Find(doc => doc.PharmacyId == PharmacyId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacyMadicine is null)
        {
            return null;
        }

        PharmacyMadicineDto pharmacyMadicineDto = Mappers.ConvertPharmacyMedicineToPharmacyMedicineDto(pharmacyMadicine);

        return pharmacyMadicineDto;
    }
    
    public async Task<PharmacyMadicineDto?> GetByMedicineIdAsync(string MedicineId, CancellationToken cancellationToken)
    {
        PharmacyMadicine pharmacyMadicine = await _collection.Find(doc => doc.MedicineId == MedicineId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacyMadicine is null)
        {
            return null;
        }

        PharmacyMadicineDto pharmacyMadicineDto = Mappers.ConvertPharmacyMedicineToPharmacyMedicineDto(pharmacyMadicine);

        return pharmacyMadicineDto;
    }

    public async Task<PharmacyMadicineDto?> UpdateByIdAsync(string userId, PharmacyMadicine userInput, CancellationToken cancellationToken)
    {
        PharmacyMadicine pharmacyMadicine = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacyMadicine is null)
        {
            return null;
        }

        if (pharmacyMadicine.Price == userInput.Price && pharmacyMadicine.Inventory == userInput.Inventory)
        {
            return null;
        }

        UpdateDefinition<PharmacyMadicine> updateDef = Builders<PharmacyMadicine>.Update.Set(doc => doc.Price,userInput.Price)
        .Set(doc => doc.Inventory, userInput.Inventory);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        PharmacyMadicine UpdatePharmacyMadicine = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (UpdatePharmacyMadicine is null)
        {
            return null;
        }

        PharmacyMadicineDto pharmacyMadicineDto = Mappers.ConvertPharmacyMedicineToPharmacyMedicineDto(UpdatePharmacyMadicine);

        return pharmacyMadicineDto;
    }

    public async Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken)
    {
        PharmacyMadicine pharmacyMadicine = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacyMadicine is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}