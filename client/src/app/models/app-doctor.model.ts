import { Address } from "./address.model";
import { Information } from "./information.model";

export interface AppDoctor {
    information: Information;
    email: string,
    medicalLicenseNumber: string,
    specialty: string,
    yearsOfExperience: number,
    clinicName: string,
    clinicAddress: Address;
    clinicNumber: string
}