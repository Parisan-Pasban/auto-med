using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.InterFaces;

public interface IUserMemberRepository
{
    public Task<List<AppUser>?> GetAllUserAsync(CancellationToken cancellationToken);
    public Task<MemberDto?> GetByLastNameAsync(string userInput, CancellationToken cancellationToken);
}