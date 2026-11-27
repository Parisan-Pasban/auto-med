namespace api.DTOs;

public record MemberDto(
    string FirstName,
    string LastName,
    string Gender,
    string PhoneNumber,
    string BirthDate,
    string BloodGroup,
    AddressDto Address
);