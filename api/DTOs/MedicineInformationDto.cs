namespace api.DTOs;

public record MedicineInformationDto
(
    string GenericName,
    string BrandName,
    string Form,
    string Dosage,
    string ManuFacturer,
    string CountryOfOrigin,
    bool RequiresPrescription
    );