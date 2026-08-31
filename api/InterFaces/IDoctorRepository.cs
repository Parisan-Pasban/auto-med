using api.DTOs;
using api.Models;

namespace api.InterFaces;

public interface IDoctorRepository
{
    public Task<DoctorInformationDto?> UpdateDoctorByIdAsync(string userId, AppDoctor userInput, CancellationToken cancellationToken);
}