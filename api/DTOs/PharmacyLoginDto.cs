namespace api.DTOs;
    public record PharmacyLoginDtp
    (
        string Name,
        string LicenseNumber,
        string Password
    );