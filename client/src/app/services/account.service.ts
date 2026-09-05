import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AppUser } from '../models/app-user.model';
import { Observable } from 'rxjs';
import { LoggedIn } from '../models/logged-in.model';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private _http = inject(HttpClient);

  private _BaseApiUrl: string = 'http://localhost:5000/api/useraccount'

  register(userInput: AppUser): Observable<LoggedIn> {
    let result$ = this._http.post<LoggedIn>(this._BaseApiUrl + 'register', userInput);

    return result$;
  }

  login(userInput: Login): Observable<LoggedIn> {
    let result$ = this._http.post<LoggedIn>(this._BaseApiUrl + 'login', userInput);

    return result$;
  }
}
