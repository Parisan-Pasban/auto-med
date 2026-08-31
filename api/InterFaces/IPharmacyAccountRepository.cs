using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface IPharmacyAccountRepository
{
    public Task<PharmacyLoggedInDto?> RegisterPharmacyAsync(Pharmacy userInput, CancellationToken cancellationToken);

    public Task<PharmacyLoggedInDto?> LoginPharmacyAsync(PharmacyLoginDtp userInput, CancellationToken cancellationToken);

    public Task<List<Pharmacy>?> GetAllPharmacyAsync(CancellationToken cancellationToken);

    public Task<PharmacyMemberDto?> GetByCityAsync(string userInput, CancellationToken cancellationToken);
    
    public Task<PharmacyMemberDto?> UpdatePharmacyByIdAsync(string userId, Pharmacy userInput, CancellationToken cancellationToken);
 
    public Task<DeleteResult?> DeletePharmacyByIdAsync(string userId, CancellationToken cancellationToken);
}
