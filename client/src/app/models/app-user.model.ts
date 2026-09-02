import { Address } from "./address.model";
import { Information } from "./information.model";

export interface AppUser {
    information: Information;
    address: Address;
    birthDate: string,
    bloodGroup: string
}