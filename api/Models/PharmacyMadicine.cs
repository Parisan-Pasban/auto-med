using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record PharmacyMadicine
(
[property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
string PharmacyId,
string MedicineId,
decimal Price,
int Inventory
);
/*
آیدی داروخونه
آیدی دارو
قیمت کالا
موجودی کالا
*/