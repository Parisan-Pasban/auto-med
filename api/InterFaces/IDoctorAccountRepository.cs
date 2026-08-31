using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;
    public interface IDoctorAccountRepository
    {
        public Task<LoggedInDto?> RegisterDoctorAsync(AppDoctor userInput, CancellationToken cancellationToken);

        public Task<LoggedInDto?> LoginDoctorAsync(LoginDto userInput, CancellationToken cancellationToken);

        public Task<DeleteResult?> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken);
    }