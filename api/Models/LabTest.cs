using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record LabTest
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Name,
    string LaboratoryId,
    string Description,
    decimal Price
);
/*
اسم آزمایش
آیدی آزمایشگاه
توضیحات آزمایش
قیمت آزمایش
*/