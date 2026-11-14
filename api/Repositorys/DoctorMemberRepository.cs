using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class DoctorMemberRepository : IDoctorMemberRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<AppDoctor> _collection;

    // constructor - dependency injections
    public DoctorMemberRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppDoctor>("doctors");
    }
    #endregion
    public async Task<List<AppDoctor>?> GetAllDoctorAsync(CancellationToken cancellationToken)
    {
        List<AppDoctor> appDoctors = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (appDoctors.Count == 0)
        {
            return null;
        }

        return appDoctors;
    }

    public async Task<DoctorInformationDto?> GetByLastNameAsync(string userInput, CancellationToken cancellationToken)
    {
        AppDoctor appDoctor = await _collection.Find(doc => doc.Information.LastName == userInput).FirstOrDefaultAsync(cancellationToken);

        if (appDoctor is null)
        {
            return null;
        }

        DoctorInformationDto informationDto = Mappers.ConvertAppDoctorToDoctorInformationDto(appDoctor, appDoctor.Information);

        return informationDto;
    }

    public async Task<DoctorInformationDto?> GetBySpecialtyAsync(string userInput, CancellationToken cancellationToken)
    {
        AppDoctor appDoctor = await _collection.Find(doc => doc.Specialty == userInput).FirstOrDefaultAsync(cancellationToken);

        if (appDoctor is null)
        {
            return null;
        }
        DoctorInformationDto informationDto = Mappers.ConvertAppDoctorToDoctorInformationDto(appDoctor, appDoctor.Information);

        return informationDto;
    }
}