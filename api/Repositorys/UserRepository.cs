using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositorys;
    public class UserRepository : IUserRepository
    {
        #region Db and Token Settings<
        private readonly IMongoCollection<AppUser> _collection;

        // constructor - dependency injections
        public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
        {
            var dbName = client.GetDatabase(dbSettings.DatabaseName);
            _collection = dbName.GetCollection<AppUser>("users");
        }
        #endregion
        public async Task<MemberDto?> UpdateUserByIdAsync(string userId, AppUser userInput, CancellationToken cancellationToken)
        {
            AppUser appUser = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

            if (appUser is null)
            {
                return null;
            }

            if (appUser.Information.PhoneNumber == userInput.Information.PhoneNumber && appUser.Address == userInput.Address)
            {
                return null;
            }

            UpdateDefinition<AppUser> updateDef = Builders<AppUser>.Update.Set(doc => doc.Information.PhoneNumber, userInput.Information.PhoneNumber)
            .Set(doc => doc.Address, userInput.Address);

            UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

            AppUser updateUser = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

            if (updateUser is null)
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