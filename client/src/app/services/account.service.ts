import { HttpClient } from '@angular/common/http';
import { inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { AppUser } from '../models/app-user.model';
import { map, Observable } from 'rxjs';
import { LoggedIn } from '../models/logged-in.model';
import { Login } from '../models/login.model';
import { DoctorInformation } from '../models/doctor-information.model';
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';
import { PharmacyLoggedIn } from '../models/pharmacy-logged-in.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private _http = inject(HttpClient);
  router = inject(Router);
  platformId = inject(PLATFORM_ID);

  loggedInUserSig = signal<LoggedIn | null>(null)

  private _BaseApiUrl: string = 'http://localhost:5000/api/useraccount/'

  register(userInput: AppUser): Observable<LoggedIn | null> {
    let result$: Observable<LoggedIn | null> = this._http.post<LoggedIn>(this._BaseApiUrl + 'register', userInput).pipe(map(res => {
      if (res) {
        this.setCurrentUser(res);

        return res;
      }

      return null;
    }));

    this.router.navigateByUrl('')

    return result$;
  }

  login(userInput: Login): Observable<LoggedIn | null> {
    let result$: Observable<LoggedIn | null> = this._http.post<LoggedIn>(this._BaseApiUrl + 'login', userInput).pipe(map(res => {
      if (res) {
        this.setCurrentUser(res);

        return res;
      }

      return null;
    }));

    this.router.navigateByUrl('')

    return result$;
  }

  getAll(): Observable<DoctorInformation[]> {
    let result$: Observable<DoctorInformation[]> = this._http.get<DoctorInformation[]>('http://localhost:5000/api/doctormember/get-all',);

    return result$;
  }

getAllPharmacy(): Observable<PharmacyLoggedIn[]> {
  let result$: Observable<PharmacyLoggedIn[]> =
    this._http.get<PharmacyLoggedIn[]>(
      'http://localhost:5000/api/PharmacyAccount/get-all'
    );

  return result$;
}

  setCurrentUser(loggedInUser: LoggedIn): void {
    this.loggedInUserSig.set(loggedInUser);

    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('userInput', JSON.stringify(loggedInUser));
    }
  }

  logout(): void {
    this.loggedInUserSig.set(null);

    if (isPlatformBrowser(this.platformId)) {
      localStorage.clear();
    }

    this.router.navigateByUrl('account/login')
  }
}