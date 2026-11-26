using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface IAppointmentRepository
{
    public Task<AppointmentDto?> CreateAppointmentAsync(Appointment userInput, CancellationToken cancellationToken);
    public Task<List<Appointment>?> GetAllAppointmentAsync(CancellationToken cancellationToken);
    public Task<AppointmentDto?> GetByDateAsync(DateTime userInput, CancellationToken cancellationToken);
    public Task<AppointmentDto?> UpdateAppointmentByIdAsync(string userId, Appointment userInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteByIdAsync(string userId, CancellationToken cancellationToken);
}