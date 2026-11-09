import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { DoctorInformation } from '../../../models/doctor-information.model';
import { Observable } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [MatCardModule, MatIconModule, MatButtonModule],
  templateUrl: './doctor-list.component.html',
  styleUrl: './doctor-list.component.scss'
})
export class DoctorListComponent implements OnInit {

  accountService = inject(AccountService);

  doctors: DoctorInformation[] | undefined;

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    let allDoctors: Observable<DoctorInformation[]> = this.accountService.getAll();

    allDoctors.subscribe({
      next: (res) => {
        console.log(res);
        this.doctors = res;
      }
    });
  }
}