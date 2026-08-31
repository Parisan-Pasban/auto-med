namespace api.DTOs;

public record PharmacyMemberDto
(
    string Name,
    string PharmacistName,
    string PharmaciNumber,
    AddressDto PharmacyAddress
);
