import { Component, Inject, inject } from '@angular/core';
import { LoggedIn } from '../../../models/logged-in.model';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppUser } from '../../../models/app-user.model';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatRadioModule,
    MatDatepickerModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  http = inject(HttpClient);
  fB = inject(FormBuilder);
  accountService = Inject(AccountService);

  userRes: LoggedIn | undefined;

  // #region registerFg
  registerFg = this.fB.group({
    information: this.fB.group({
      firstNameCtrl: '',
      lastNameCtrl: '',
      nationalCodeCtrl: '',
      genderCtrl: '',
      phoneNumberCtrl: '',
      passwordCtrl: '',
      confirmPasswordCtrl: ''
    }),
    address: this.fB.group({
      cityCtrl: '',
      streetCtrl: '',
      alleyCtrl: '',
      plaqueCtrl: '',
      unitCtrl: ''
    }),
    birthDateCtrl: '',
    bloodGroupCtrl: ''
  });

  get FirstNameCtrl(): FormControl {
    return this.registerFg.get('information.firstNameCtrl') as FormControl;
  }

  get LastNameCtrl(): FormControl {
    return this.registerFg.get('information.lastNameCtrl') as FormControl;
  }

  get NationalCodeCtrl(): FormControl {
    return this.registerFg.get('information.nationalCodeCtrl') as FormControl;
  }

  get GenderCtrl(): FormControl {
    return this.registerFg.get('information.genderCtrl') as FormControl;
  }

  get PhoneNumberCtrl(): FormControl {
    return this.registerFg.get('information.phoneNumberCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.registerFg.get('information.passwordCtrl') as FormControl;
  }

  get ConfirmPasswordCtrl(): FormControl {
    return this.registerFg.get('information.confirmPasswordCtrl') as FormControl;
  }

  get CityCtrl(): FormControl {
    return this.registerFg.get('address.cityCtrl') as FormControl;
  }

  get StreetCtrl(): FormControl {
    return this.registerFg.get('address.streetCtrl') as FormControl
  }

  get AlleyCtrl(): FormControl {
    return this.registerFg.get('address.alleyCtrl') as FormControl;
  }

  get PlaqueCtrl(): FormControl {
    return this.registerFg.get('address.plaqueCtrl') as FormControl;
  }

  get UnitCtrl(): FormControl {
    return this.registerFg.get('address.unitCtrl') as FormControl;
  }

  get BirthDateCtrl(): FormControl {
    return this.registerFg.get('birthDateCtrl') as FormControl;
  }

  get BloodGroupCtrl(): FormControl {
    return this.registerFg.get('bloodGroupCtrl') as FormControl
  }
  // #endregion

  register(): void {
    let appUser: AppUser = {
      information: {
        firstName: this.FirstNameCtrl.value,
        lastName: this.LastNameCtrl.value,
        nationalCode: this.NationalCodeCtrl.value,
        gender: this.GenderCtrl.value,
        phoneNumber: this.PhoneNumberCtrl.value,
        password: this.PasswordCtrl.value,
        confirmPassword: this.ConfirmPasswordCtrl.value
      },
      address: {
        city: this.CityCtrl.value,
        street: this.StreetCtrl.value,
        alley: this.AlleyCtrl.value,
        plaque: this.PlaqueCtrl.value,
        unit: this.UnitCtrl.value
      },
      birthDate: this.BirthDateCtrl.value,
      bloodGroup: this.BloodGroupCtrl.value
    }
    
    this.accountService.register(appUser).subscribe({
      next: (res: LoggedIn) => {
        console.log(res);

        this.userRes = res;
      }
    });
  }
}