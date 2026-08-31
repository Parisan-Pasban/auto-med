using api.DTOs;
using api.Models;

namespace api.InterFaces;
public interface IUserRepository
{
    public Task<MemberDto?> UpdateUserByIdAsync(string userId, AppUser userInput, CancellationToken cancellationToken);
}