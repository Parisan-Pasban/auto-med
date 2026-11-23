namespace api.DTOs;

public record LaboratoryDto
(
    string Name,
    AddressDto AddressLaboratory,
    string PhoneNumber
);