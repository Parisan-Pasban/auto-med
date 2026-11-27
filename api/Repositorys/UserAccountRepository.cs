using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositorys;

public class UserAccountRepository : IUserAcountRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<AppUser> _collection;

    // constructor - dependency injections
    public UserAccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>("users");
    }
    #endregion
    
    public async Task<LoggedInDto?> RegisterUserAsync(AppUser userInput, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find(doc => doc.Information.NationalCode == userInput.Information.NationalCode).FirstOrDefaultAsync(cancellationToken);

        if (appUser is not null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        LoggedInDto loggedInDto = Mappers.ConvertAppUserToLoggedInDto(userInput);

        return loggedInDto;
    }

    public async Task<LoggedInDto?> LoginUserAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find(
            doc => doc.Information.FirstName == userInput.FirstName &&
                   doc.Information.LastName == userInput.LastName &&
                   doc.Information.Password == userInput.Password
        ).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
        {
            return null;
        }

        LoggedInDto loggedInDto = Mappers.ConvertAppUserToLoggedInDto(appUser);

        return loggedInDto;
    }

    public async Task<DeleteResult?> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}