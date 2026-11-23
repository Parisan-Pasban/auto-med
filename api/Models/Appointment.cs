using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record Appointment
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string DoctorId,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int AppointmentDuration,
    int BreakDuration,
    int MaxPatients
);
/*
آیدی دکتر
تاریخ
تایم شروغ
تایم پایان
مدت زمان ویزیت
تایم استراحت
تعداد بیماران
*/