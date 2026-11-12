namespace api.DTOs;

public record AppointmentDto
(
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int AppointmentDuration,
    int BreakDuration,
    int MaxPatients
);