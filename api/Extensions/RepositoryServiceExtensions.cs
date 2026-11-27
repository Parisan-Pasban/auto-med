using api.InterFaces;
using api.Repositorys;
using api.Services;

namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        services.AddScoped<IUserAcountRepository, UserAccountRepository>();
        services.AddScoped<IDoctorAccountRepository, DoctorAccountRepository>();
        services.AddScoped<IUserMemberRepository, UserMemberRepository>();
        services.AddScoped<IDoctorMemberRepository, DoctorMemberRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPharmacyAccountRepository, PharmacyAccountRepository>();
        services.AddScoped<IMedicineAccountRepository, MedicineAccountRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        services.AddScoped<ILabTestRepository, LabTestRepository>();
        services.AddScoped<IPharmacyMadicineRepository, PharmacyMadicineRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}