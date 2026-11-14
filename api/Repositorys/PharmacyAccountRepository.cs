using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class PharmacyAccountRepository : IPharmacyAccountRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<Pharmacy> _collection;

    // constructor - dependency injections
    public PharmacyAccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Pharmacy>("pharmacys");
    }

    #endregion
    public async Task<PharmacyLoggedInDto?> RegisterPharmacyAsync(Pharmacy userInput, CancellationToken cancellationToken)
    {
        Pharmacy pharmacy = await _collection.Find(doc => doc.LicenseNumber == userInput.LicenseNumber).FirstOrDefaultAsync(cancellationToken);

        if (pharmacy is not null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        PharmacyLoggedInDto pharmacyLoggedInDto = Mappers.ConvertPharmacyToPharmacyLoggedInDto(userInput);

        return pharmacyLoggedInDto;
    }
    public async Task<PharmacyLoggedInDto?> LoginPharmacyAsync(PharmacyLoginDtp userInput, CancellationToken cancellationToken)
    {
        Pharmacy pharmacy = await _collection.Find(doc => doc.Name == userInput.Name && doc.LicenseNumber == userInput.LicenseNumber && doc.Password == userInput.Password).FirstOrDefaultAsync(cancellationToken);

        if (pharmacy is null)
        {
            return null;
        }

        PharmacyLoggedInDto pharmacyLoggedInDto = Mappers.ConvertPharmacyToPharmacyLoggedInDto(pharmacy);

        return pharmacyLoggedInDto;
    }

    public async Task<List<Pharmacy>?> GetAllPharmacyAsync(CancellationToken cancellationToken)
    {
        List<Pharmacy> pharmacies = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (pharmacies.Count == 0)
        {
            return null;
        }

        return pharmacies;
    }

    public async Task<PharmacyMemberDto?> GetByCityAsync(string userInput, CancellationToken cancellationToken)
    {
        Pharmacy pharmacy = await _collection.Find(doc => doc.AddressPharmacy.City == userInput).FirstOrDefaultAsync(cancellationToken);

        if (pharmacy is null)
        {
            return null;
        }

        PharmacyMemberDto pharmacyMemberDto = Mappers.ConvertPharmacyToPharmacyMemberDto(pharmacy);

        return pharmacyMemberDto;
    }

    public async Task<PharmacyMemberDto?> UpdatePharmacyByIdAsync(string userId, Pharmacy userInput, CancellationToken cancellationToken)
    {
        Pharmacy pharmacy = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacy is null)
        {
            return null;
        }

        if (pharmacy.PharmacistName == userInput.PharmacistName && pharmacy.PersonalNumber == userInput.PersonalNumber)
        {
            return null;
        }

        UpdateDefinition<Pharmacy> updateDef = Builders<Pharmacy>.Update.Set(doc => doc.PharmacistName, userInput.PharmacistName)
        .Set(doc => doc.PersonalNumber, userInput.PersonalNumber);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        Pharmacy updatePharmacy = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (updatePharmacy is null)
        {
            return null;
        }

        PharmacyMemberDto pharmacyMemberDto = Mappers.ConvertPharmacyToPharmacyMemberDto(pharmacy);

        return pharmacyMemberDto;
    }

    public async Task<DeleteResult?> DeletePharmacyByIdAsync(string userId, CancellationToken cancellationToken)
    {
        Pharmacy pharmacy = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (pharmacy is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}