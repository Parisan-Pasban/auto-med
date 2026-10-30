import { HttpClient } from '@angular/common/http';
import { inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { AppUser } from '../models/app-user.model';
import { map, Observable } from 'rxjs';
import { LoggedIn } from '../models/logged-in.model';
import { Login } from '../models/login.model';
import { DoctorInformation } from '../models/doctor-information.model';
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private _http = inject(HttpClient);
  router = inject(Router);
  platformId = inject(PLATFORM_ID);

  loggedInUserUserSig =  signal<LoggedIn | null>(null )

  private _BaseApiUrl: string = 'http://localhost:5000/api/useraccount/'

  register(userInput: AppUser): Observable<LoggedIn | null> {
    let result$: Observable<LoggedIn | null> = this._http.post<LoggedIn>(this._BaseApiUrl + 'register', userInput).pipe(map(res => {
      if (res) {
        this.setCurrentUser(res);

        return res;
      }

      return null;
    }));

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

    return result$;
  }

  getAll(): Observable<DoctorInformation[]> {
    let result$: Observable<DoctorInformation[]> = this._http.get<DoctorInformation[]>('http://localhost:5000/api/doctormember/get-all',);

    return result$;
  }

  setCurrentUser(loggedInUser: LoggedIn): void {
    this.loggedInUserUserSig.set(loggedInUser);
    
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('userInput', JSON.stringify(loggedInUser));
    }
  }

  logout(): void {
    this.loggedInUserUserSig.set(null);
    
    if (isPlatformBrowser(this.platformId)) {
      localStorage.clear();
    }

    this.router.navigateByUrl('account/login')
  }
}