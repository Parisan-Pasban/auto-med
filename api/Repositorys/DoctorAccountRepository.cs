using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositorys;
    public class DoctorAccountRepository : IDoctorAccountRepository
    {
        #region Db and Token Settings
        private readonly IMongoCollection<AppDoctor> _collection;

        // constructor - dependency injections
        public DoctorAccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
        {
            var dbName = client.GetDatabase(dbSettings.DatabaseName);
            _collection = dbName.GetCollection<AppDoctor>("doctors");
        }
        #endregion
        public async Task<LoggedInDto?> RegisterDoctorAsync(AppDoctor userInput, CancellationToken cancellationToken)
        {
            AppDoctor appDoctor = await _collection.Find(doc => doc.Information.NationalCode == userInput.Information.NationalCode).FirstOrDefaultAsync(cancellationToken);

            if (appDoctor is not null)
            {
                return null;
            }

            await _collection.InsertOneAsync(userInput, null, cancellationToken);

            LoggedInDto loggedInDto = new LoggedInDto(
                FirstName: userInput.Information.FirstName,
                LastName: userInput.Information.LastName
            );

            return loggedInDto;
        }

        public async Task<LoggedInDto?> LoginDoctorAsync(LoginDto userInput, CancellationToken cancellationToken)
        {
            AppDoctor appDoctor = await _collection.Find(doc => doc.Information.FirstName == userInput.FirstName && doc.Information.LastName == userInput.LastName && doc.Information.Password == userInput.Password).FirstOrDefaultAsync(cancellationToken);

            if (appDoctor is null)
            {
                return null;
            }

            LoggedInDto loggedInDto = new LoggedInDto(
                FirstName: userInput.FirstName,
                LastName: userInput.LastName
           );

            return loggedInDto;
        }

        public async Task<DeleteResult?> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken)
        {
            AppDoctor appDoctor = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

            if (appDoctor is null)
            {
                return null;
            }

            return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
        }
    }