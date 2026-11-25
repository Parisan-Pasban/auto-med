using api.Models;

namespace api.InterFaces;

public interface ITokenService
{
    string CreateToken(AppDoctor user);
}