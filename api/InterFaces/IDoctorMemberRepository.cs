using api.DTOs;
using api.Models;

namespace api.InterFaces;

public interface IDoctorMemberRepository
{
    public Task<List<AppDoctor>?> GetAllDoctorAsync(CancellationToken cancellationToken);
    
    public Task<DoctorInformationDto?> GetByLastNameAsync(string userInput, CancellationToken cancellationToken);

    public Task<DoctorInformationDto?> GetBySpecialtyAsync(string userInput, CancellationToken cancellationToken);
}