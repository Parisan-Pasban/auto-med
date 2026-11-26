using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositorys;

public class DoctorRepository : IDoctorRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<AppDoctor> _collection;

    public DoctorRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppDoctor>("doctors");
    }

    #endregion
    public async Task<DoctorInformationDto?> UpdateDoctorByIdAsync(string userId, AppDoctor userInput, CancellationToken cancellationToken)
    {
        AppDoctor appDoctor = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appDoctor is null)
        {
            return null;
        }

        if (appDoctor.Information.Email == userInput.Information.Email && appDoctor.ClinicAddress == userInput.ClinicAddress && appDoctor.ClinicNumber == userInput.ClinicNumber && appDoctor.ClinicName == userInput.ClinicName)
        {
            return null;
        }

        UpdateDefinition<AppDoctor> updateDef = Builders<AppDoctor>.Update.Set(doc => doc.Information.Email, userInput.Information.Email)
        .Set(doc => doc.ClinicAddress, userInput.ClinicAddress)
        .Set(doc => doc.ClinicNumber, userInput.ClinicNumber)
        .Set(doc => doc.ClinicName, userInput.ClinicName);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        AppDoctor updateDoctor = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (updateDoctor is null)
        {
            return null;
        }

        DoctorInformationDto informationDto = Mappers.ConvertAppDoctorToDoctorInformationDto(appDoctor, appDoctor.Information);

        return informationDto;
    }
}