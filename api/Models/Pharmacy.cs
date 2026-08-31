using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record Pharmacy
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Email,
    string Name,
    string LicenseNumber,
    string PharmacistName,
    string PersonalNumber,
    string PharmaciNumber,
    string Password,
    string ConfirmPassword,
    Address AddressPharmacy
);
/*
ایمیل
اسم داروخونه
شماره مجوز
نام داروساز
شماره شخصی برای ارتباظ
شماره داروخونه
آدرس
*/