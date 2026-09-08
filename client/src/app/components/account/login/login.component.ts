import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { LoggedIn } from '../../../models/logged-in.model';
import { Login } from '../../../models/login.model';
import { AccountService } from '../../../services/account.service';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatRadioModule,
    RouterLink
],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  http = inject(HttpClient);
  fb = inject(FormBuilder);
  accountService = inject(AccountService);

  userRes: LoggedIn | undefined;

  // #region loginFg
  loginFg = this.fb.group({
    firstNameCtrl: '',
    lastNameCtrl: '',
    passwordCtrl: ''
  });

  get FirstNameCtrl(): FormControl {
    return this.loginFg.get('firstNameCtrl') as FormControl;
  }

  get LastNameCtrl(): FormControl {
    return this.loginFg.get('lastNameCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.loginFg.get('passwordCtrl') as FormControl;
  }
  // #endregion

  login(): void {
    let login: Login = {
      firstName: this.FirstNameCtrl.value,
      lastName: this.LastNameCtrl.value,
      password: this.PasswordCtrl.value
    }

    this.accountService.login(login).subscribe({
      next: (res: LoggedIn) => {
        console.log(res);

        this.userRes = res;
      }
    });
  }
}