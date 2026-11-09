import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { Observable } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { PharmacyLoggedIn } from '../../../models/pharmacy-logged-in.model';

@Component({
  selector: 'app-pharmacy-list',
  imports: [MatCardModule, MatIconModule, MatButtonModule],
  templateUrl: './pharmacy-list.component.html',
  styleUrl: './pharmacy-list.component.scss'
})
export class PharmacyListComponent implements OnInit {

  accountService = inject(AccountService);

  pharmacies: PharmacyLoggedIn[] | undefined;

  ngOnInit(): void {
    this.getAllPharmacy();
  }

  getAllPharmacy(): void {
    let allPharmacy: Observable<PharmacyLoggedIn[]> = this.accountService.getAllPharmacy();

    allPharmacy.subscribe({
      next: (res) => {
        console.log(res);
        this.pharmacies = res;
      }
    });
  }
}
