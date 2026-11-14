using api.DTOs;
using api.Models;
using MongoDB.Driver;

namespace api.InterFaces;

public interface IAppointmentRepository
{
    public Task<AppointmentDto?> CreateAppointment(Appointment userInput, CancellationToken cancellationToken);

    public Task<List<Appointment>?> GetAllAppointment(CancellationToken cancellationToken);

    public Task<AppointmentDto?> GetByDate(DateTime userInput, CancellationToken cancellationToken);

    public Task<AppointmentDto?> UpdateAppointmentById(string userId, Appointment userInput, CancellationToken cancellationToken);

    public Task<DeleteResult?> DeleteById(string userId, CancellationToken cancellationToken);

}