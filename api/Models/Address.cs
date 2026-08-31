namespace api.Models;

public record Address(
    string City,
    string Street,
    string Alley,
    string Plaque,
    string Unit
);
