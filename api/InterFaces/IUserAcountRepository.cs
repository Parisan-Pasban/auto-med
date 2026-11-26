using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;
    public interface IUserAcountRepository
    {
        public Task<LoggedInDto?> RegisterUserAsync(AppUser userInput, CancellationToken cancellationToken);
        public Task<LoggedInDto?> LoginUserAsync(LoginDto userInput, CancellationToken cancellationToken);
        public Task<DeleteResult?> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken);
    }