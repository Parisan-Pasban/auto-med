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

    // constructor - dependency injections
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

        if (appDoctor.Email == userInput.Email && appDoctor.ClinicAddress == userInput.ClinicAddress && appDoctor.ClinicNumber == userInput.ClinicNumber && appDoctor.ClinicName == userInput.ClinicName)
        {
            return null;
        }

        UpdateDefinition<AppDoctor> updateDef = Builders<AppDoctor>.Update.Set(doc => doc.Email, userInput.Email)
        .Set(doc => doc.ClinicAddress, userInput.ClinicAddress)
        .Set(doc => doc.ClinicNumber, userInput.ClinicNumber)
        .Set(doc => doc.ClinicNumber, userInput.ClinicNumber)
        .Set(doc => doc.ClinicName, userInput.ClinicName);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        AppDoctor updateDoctor = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (updateDoctor is null)
        {
            return null;
        }

        DoctorInformationDto informationDto = new DoctorInformationDto(
           FirstName: appDoctor.Information.FirstName,
           LastName: appDoctor.Information.LastName,
           Gender: appDoctor.Information.Gender,
           PhoneNumber: appDoctor.Information.PhoneNumber,
           Specialty: appDoctor.Specialty,
           ClinicName: appDoctor.ClinicName,
           ClinicNumber: appDoctor.ClinicNumber,
           ClinicAddress: new AddressDto(
                 City: appDoctor.ClinicAddress.City,
                Street: appDoctor.ClinicAddress.Street,
                Alley: appDoctor.ClinicAddress.Alley,
                Plaque: appDoctor.ClinicAddress.Plaque,
                Unit: appDoctor.ClinicAddress.Unit
           )
       );

        return informationDto;
    }
}