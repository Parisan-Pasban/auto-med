using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class MedicineAccountRepository : IMedicineAccountRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<Medicine> _collection;

    // constructor - dependency injections
    public MedicineAccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Medicine>("medicines");
    }
    #endregion
    public async Task<MedicineInformationDto?> RegisterMedicineAsync(Medicine userInput, CancellationToken cancellationToken)
    {
        Medicine medicine = await _collection.Find(doc => doc.GenericName == userInput.GenericName).FirstOrDefaultAsync(cancellationToken);

        if (medicine is not null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        MedicineInformationDto medicineInformationDto = new MedicineInformationDto(
            GenericName: userInput.GenericName,
            BrandName: userInput.BrandName,
            Form: userInput.Form,
            Dosage: userInput.Dosage,
            ManuFacturer: userInput.ManuFacturer,
            CountryOfOrigin: userInput.CountryOfOrigin,
            Price: userInput.Price,
            Inventory: userInput.Inventory,
            RequiresPrescription: userInput.RequiresPrescription
        );

        return medicineInformationDto;
    }

    public async Task<List<Medicine>?> GetAllMedicineAsync(CancellationToken cancellationToken)
    {
        List<Medicine> medicines = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (medicines.Count == 0)
        {
            return null;
        }

        return medicines;
    }

    public async Task<MedicineInformationDto?> GetByGenericNameAsync(string userInput, CancellationToken cancellationToken)
    {
        Medicine medicine = await _collection.Find(doc => doc.GenericName == userInput).FirstOrDefaultAsync(cancellationToken);

        if (medicine is null)
        {
            return null;
        }

        MedicineInformationDto medicineInformationDto = new MedicineInformationDto(
            GenericName: medicine.GenericName,
            BrandName: medicine.BrandName,
            Form: medicine.Form,
            Dosage: medicine.Dosage,
            ManuFacturer: medicine.ManuFacturer,
            CountryOfOrigin: medicine.CountryOfOrigin,
            Price: medicine.Price,
            Inventory: medicine.Inventory,
            RequiresPrescription: medicine.RequiresPrescription
        );

        return medicineInformationDto;
    }

    public async Task<MedicineInformationDto?> GetByGenericFormAsync(string userInput, CancellationToken cancellationToken)
    {
        Medicine medicine = await _collection.Find(doc => doc.Form == userInput).FirstOrDefaultAsync(cancellationToken);

        if (medicine is null)
        {
            return null;
        }

        MedicineInformationDto medicineInformationDto = new MedicineInformationDto(
            GenericName: medicine.GenericName,
            BrandName: medicine.BrandName,
            Form: medicine.Form,
            Dosage: medicine.Dosage,
            ManuFacturer: medicine.ManuFacturer,
            CountryOfOrigin: medicine.CountryOfOrigin,
            Price: medicine.Price,
            Inventory: medicine.Inventory,
            RequiresPrescription: medicine.RequiresPrescription
        );

        return medicineInformationDto;
    }

    public async Task<MedicineInformationDto?> UpdateMedicineByIdAsync(string userId, Medicine userInput, CancellationToken cancellationToken)
    {
        Medicine medicine = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (medicine is null)
        {
            return null;
        }

        if (medicine.Form == userInput.Form && medicine.Dosage == userInput.Dosage && medicine.Price == userInput.Price && medicine.Inventory == userInput.Inventory)
        {
            return null;
        }

        UpdateDefinition<Medicine> updateDef = Builders<Medicine>.Update.Set(doc => doc.Form, userInput.Form)
        .Set(doc => doc.Dosage, userInput.Dosage)
        .Set(doc => doc.Price, userInput.Price)
        .Set(doc => doc.Inventory, userInput.Inventory);

        UpdateResult Result = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        Medicine updateResault = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (updateResault is null)
        {
            return null;
        }


        MedicineInformationDto medicineInformationDto = new MedicineInformationDto(
            GenericName: medicine.GenericName,
            BrandName: medicine.BrandName,
            Form: medicine.Form,
            Dosage: medicine.Dosage,
            ManuFacturer: medicine.ManuFacturer,
            CountryOfOrigin: medicine.CountryOfOrigin,
            Price: medicine.Price,
            Inventory: medicine.Inventory,
            RequiresPrescription: medicine.RequiresPrescription
        );

        return medicineInformationDto;
    }

    public async Task<DeleteResult?> deleteMedicineByIdAsync(string userId, CancellationToken cancellationToken)
    {
        Medicine medicine = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (medicine is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}