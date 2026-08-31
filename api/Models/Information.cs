namespace api.Models;

public record Information
(
    string FirstName,
    string LastName,
    string NationalCode,
    string Gender,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
);