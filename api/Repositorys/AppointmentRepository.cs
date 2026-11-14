using api.DTOs;
using api.InterFaces;
using api.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositorys;

public class AppointmentRepository : IAppointmentRepository
{
    #region Db and Token Settings
    private readonly IMongoCollection<Appointment> _collection;

    // constructor - dependency injections
    public AppointmentRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Appointment>("Appointment");
    }

    #endregion
    public async Task<AppointmentDto?> CreateAppointment(Appointment userInput, CancellationToken cancellationToken)
    {
        Appointment appointment = await _collection.Find(doc => doc.DoctorId == userInput.DoctorId).FirstOrDefaultAsync(cancellationToken);

        if (appointment is null)
        {
            return null;
        }

        await _collection.InsertOneAsync(userInput, null, cancellationToken);

        AppointmentDto appointmentDto = Mappers.ConvertAppointmentToAppointmentDto(userInput);

        return appointmentDto;
    }


    public async Task<List<Appointment>?> GetAllAppointment(CancellationToken cancellationToken)
    {
        List<Appointment> appointments = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken);

        if (appointments.Count == 0)
        {
            return null;
        }

        return appointments;
    }

    public async Task<AppointmentDto?> GetByDate(DateTime userInput, CancellationToken cancellationToken)
    {
        Appointment appointment = await _collection.Find(doc => doc.Date == userInput).FirstOrDefaultAsync(cancellationToken);

        if (appointment is null)
        {
            return null;
        }

        AppointmentDto appointmentDto = Mappers.ConvertAppointmentToAppointmentDto(appointment);

        return appointmentDto;
    }

    public async Task<AppointmentDto?> UpdateAppointmentById(string userId, Appointment userInput, CancellationToken cancellationToken)
    {
        Appointment appointment = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appointment is null)
        {
            return null;
        }

        if (appointment.Date == userInput.Date && appointment.StartTime == userInput.StartTime && appointment.EndTime == userInput.EndTime && appointment.AppointmentDuration == userInput.AppointmentDuration && appointment.BreakDuration == userInput.BreakDuration && appointment.MaxPatients == userInput.MaxPatients)
        {
            return null;
        }

        UpdateDefinition<Appointment> updateDef = Builders<Appointment>.Update.Set(doc => doc.Date, userInput.Date)
        .Set(doc => doc.StartTime, userInput.StartTime)
        .Set(doc => doc.EndTime, userInput.EndTime)
        .Set(doc => doc.AppointmentDuration, userInput.AppointmentDuration)
        .Set(doc => doc.BreakDuration, userInput.BreakDuration)
        .Set(doc => doc.MaxPatients, userInput.MaxPatients);

        UpdateResult updateResult = await _collection.UpdateOneAsync(doc => doc.Id == userId, updateDef, null, cancellationToken);

        Appointment updateAppointment = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (updateAppointment is null)
        {
            return null;
        }

        AppointmentDto appointmentDto = Mappers.ConvertAppointmentToAppointmentDto(appointment);

        return appointmentDto;
    }
    public async Task<DeleteResult?> DeleteById(string userId, CancellationToken cancellationToken)
    {
        Appointment appointment = await _collection.Find(doc => doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appointment is null)
        {
            return null;
        }

        return await _collection.DeleteOneAsync(doc => doc.Id == userId, cancellationToken);
    }
}