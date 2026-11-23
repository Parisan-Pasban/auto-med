using api.Models;

namespace api.DTOs;

public static class Mappers
{
    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser)
    {
        return new(
            FirstName: appUser.Information.FirstName,
            LastName: appUser.Information.LastName,
            Gender: appUser.Information.Gender
        );
    }

    public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
    {
        return new(
            FirstName: appUser.Information.FirstName,
            LastName: appUser.Information.LastName,
            Gender: appUser.Information.Gender,
            PhoneNumber: appUser.Information.PhoneNumber,
            BirthDate: appUser.BirthDate,
            BloodGroup: appUser.BloodGroup,
            Address: new AddressDto(
                City: appUser.Address.City,
                Street: appUser.Address.Street,
                Alley: appUser.Address.Alley,
                Plaque: appUser.Address.Plaque,
                Unit: appUser.Address.Unit
            )
        );
    }

    public static LoggedInDto ConvertAppDoctorToLoggedInDto(AppDoctor appDoctor)
    {
        return new(
            FirstName: appDoctor.Information.FirstName,
            LastName: appDoctor.Information.LastName,
            Gender: appDoctor.Information.Gender
        );
    }

    public static DoctorInformationDto ConvertAppDoctorToDoctorInformationDto(AppDoctor appDoctor, Information information)
    {
        return new(
            FirstName: information.FirstName,
            LastName: information.LastName,
            Gender: information.Gender,
            PhoneNumber: information.PhoneNumber,
            Specialty: appDoctor.Specialty,
            ClinicName: appDoctor.ClinicName,
            ClinicNumber: appDoctor.ClinicNumber,

            ClinicAddress: new AddressDto
            (
                 City: appDoctor.ClinicAddress.City,
                Street: appDoctor.ClinicAddress.Street,
                Alley: appDoctor.ClinicAddress.Alley,
                Plaque: appDoctor.ClinicAddress.Plaque,
                Unit: appDoctor.ClinicAddress.Unit
            )
        );
    }

    public static PharmacyLoggedInDto ConvertPharmacyToPharmacyLoggedInDto(Pharmacy pharmacy)
    {
        return new(
            Name: pharmacy.Name,
            PharmacistName: pharmacy.PharmacistName
        );
    }

    public static PharmacyMemberDto ConvertPharmacyToPharmacyMemberDto(Pharmacy pharmacy)
    {
        return new(
            Name: pharmacy.Name,
            PharmacistName: pharmacy.PharmacistName,
            PharmaciNumber: pharmacy.PharmaciNumber,
            PharmacyAddress: new AddressDto(
                    City: pharmacy.AddressPharmacy.City,
                    Street: pharmacy.AddressPharmacy.Street,
                    Alley: pharmacy.AddressPharmacy.Alley,
                    Plaque: pharmacy.AddressPharmacy.Plaque,
                    Unit: pharmacy.AddressPharmacy.Unit
            )
        );
    }

    public static MedicineInformationDto ConvertMedicineToMedicineInformationDto(Medicine medicine)
    {
        return new(
            GenericName: medicine.GenericName,
            BrandName: medicine.BrandName,
            Form: medicine.Form,
            Dosage: medicine.Dosage,
            ManuFacturer: medicine.ManuFacturer,
            CountryOfOrigin: medicine.CountryOfOrigin,
            RequiresPrescription: medicine.RequiresPrescription
        );
    }

    public static AppointmentDto ConvertAppointmentToAppointmentDto(Appointment appointment)
    {
        return new(
            Date: appointment.Date,
            StartTime: appointment.StartTime,
            EndTime: appointment.EndTime,
            AppointmentDuration: appointment.AppointmentDuration,
            BreakDuration: appointment.BreakDuration,
            MaxPatients: appointment.MaxPatients
        );
    }

    public static LaboratoryDto ConvertLaboratoryToLaboratoryDto(Laboratory laboratory)
    {
        return new(
            Name: laboratory.Name,
            AddressLaboratory: new AddressDto(
                    City: laboratory.AddressLaboratory.City,
                    Street: laboratory.AddressLaboratory.Street,
                    Alley: laboratory.AddressLaboratory.Alley,
                    Plaque: laboratory.AddressLaboratory.Plaque,
                    Unit: laboratory.AddressLaboratory.Unit
            ),
            PhoneNumber: laboratory.PhoneNumber
        );
    }
}