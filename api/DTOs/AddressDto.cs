namespace api.DTOs;

public record AddressDto
(
    string City,
    string Street,
    string Alley,
    string Plaque,
    string Unit
);