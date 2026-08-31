using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class UserMemberRepository : IUserMemberRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<AppUser> _collection;

    // constructor - dependency injections
    public UserMemberRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>("users");
    }
    #endregion
    public async Task<List<AppUser>?> GetAllUserAsync(CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (appUsers.Count == 0)
        {
            return null;
        }

        return appUsers;
    }

    public async Task<MemberDto?> GetByLastNameAsync(string userInput, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find(doc => doc.Information.LastName == userInput).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
        {
            return null;
        }

        MemberDto memberDto = new MemberDto(
            FirstName: appUser.Information.FirstName,
            LastName: appUser.Information.LastName,
            Gender: appUser.Information.Gender,
            PhoneNumber: appUser.Information.PhoneNumber,
            BirthDate: appUser.BirthDate,
            BloodGroup: appUser.BloodGroup,
            Address: new AddressDto(
                City: appUser.Address.City,
                Street: appUser.Address.Street,
                Alley: appUser.Address.Alley,
                Plaque: appUser.Address.Plaque,
                Unit: appUser.Address.Unit
            )
        );

        return memberDto;
    }
}