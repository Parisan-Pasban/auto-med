namespace api.DTOs;

public record DoctorInformationDto
(
    string FirstName,
    string LastName,
    string Gender,
    string PhoneNumber,
    string Specialty,
    string ClinicName,
    AddressDto ClinicAddress,
    string ClinicNumber
);