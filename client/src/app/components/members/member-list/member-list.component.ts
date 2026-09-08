import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { DoctorInformation } from '../../../models/doctor-information.modei';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-member-list',
  imports: [],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.scss'
})
export class MemberListComponent implements OnInit {
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
      },
    });
  }
}
