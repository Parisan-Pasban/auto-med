using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record Medicine
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string GenericName,
    string BrandName,
    string Form,
    string Dosage,
    string ManuFacturer,
    string CountryOfOrigin,
    bool RequiresPrescription,
    PharmacyMadicine PharmacyMadicine
);
/*
اسم عمومی
اسم برند
شکل دارو: کپسول. شربت. آمپول
دوز دارو
شرکت سازنده
کشور سازنده
آیا برای خرید نیاز به نسخه دارد
*/