import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { AccountService } from './services/account.service';



@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  accountService = inject(AccountService);

  ngOnInit(): void {
    let loggedInUserStr: string | null = localStorage.getItem('userInput');
   
    console.log(loggedInUserStr);
    

    if (loggedInUserStr) {
      this.accountService.setCurrentUser(JSON.parse(loggedInUserStr)); 
    }
  }
}