using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record AppDoctor(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    Information Information,
    string Email,
    string MedicalLicenseNumber,
    string Specialty,
    int YearsOfExperience,
    string ClinicName,
    Address ClinicAddress,
    string ClinicNumber
);
/*
شماره مجوز پزشکی
رشته تخصص
سال های تجربه
اسم کلینیک
آدرس کلینیک
شماره تلفن کلینیک
*/