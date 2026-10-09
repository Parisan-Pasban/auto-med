import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { DoctorInformation } from '../../../models/doctor-information.model';
import { Observable } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-member-list',
  imports: [MatCardModule, MatIconModule, MatButtonModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.scss'
})
export class MemberListComponent implements OnInit {
  accountService = inject(AccountService);

  // test = 'test';

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
      },
    });
  }
}
