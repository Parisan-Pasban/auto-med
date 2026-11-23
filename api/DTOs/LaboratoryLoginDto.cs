namespace api.DTOs;

public record LaboratoryLoginDto
(
    string Name,
    string LicenseNumber,
    string Password
);