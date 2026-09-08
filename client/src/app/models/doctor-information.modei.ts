import { AddressResponse } from "./address-response.model";


export interface DoctorInformation {
    firstName: string,
    lastName: string,
    gender: string,
    phoneNumber: string,
    Specialty: string,
    clinicName: string,
    clinicAddres: AddressResponse;
}