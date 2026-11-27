namespace api.DTOs;

public record LoginDto(
    string FirstName,
    string LastName,
    string Password
);