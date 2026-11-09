import { Routes } from '@angular/router';

import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DoctorCardComponent } from './components/doctors/doctor-card/doctor-card.component';
import { DoctorListComponent } from './components/doctors/doctor-list/doctor-list.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { HomeComponent } from './components/home/home.component';
import { PharmacyCardComponent } from './components/members/member-list/member-list.component';
import { PharmacyListComponent } from './components/pharmacies/pharmacy-list/pharmacy-list.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'account/login', component: LoginComponent },
  { path: 'footer', component: FooterComponent },
  { path: 'navbar', component: NavbarComponent },
  { path: 'members/doctor-card', component: DoctorCardComponent },
  { path: 'members/doctor-list', component: DoctorListComponent },
  { path: 'pharmacies/pharmacy-card', component: PharmacyCardComponent },
  { path: 'pharmacies/pharmacy-list', component: PharmacyListComponent },
  { path: '**', component: NotFoundComponent }
];