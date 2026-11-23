using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record Laboratory
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Name,
    Address AddressLaboratory,
    string LicenseNumber,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
);
/*
نام آزمایشگاه
آدرس آزمایشکاه
شماره مجوز
شماره تماس آزمایشگاه
*/