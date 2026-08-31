using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;
    public record AppUser(
        [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
        Information Information,
        Address Address,
        string BirthDate,
        string BloodGroup
    );
    /*
    تاریخ  تولد
    گروه خونی
    */