import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  isLoggato: boolean = false;

  constructor(private router: Router) {}

  token: string | null = JSON.stringify(
    localStorage.getItem('ilToken')
  ).replace(/["]+/g, '');

  checkToken(): void {
    this.token = JSON.stringify(localStorage.getItem('ilToken')).replace(
      /["]+/g,
      ''
    );
  }

  redirectLogin(): void {
    this.router.navigateByUrl('/');
    this.checkToken();
    location.reload();
  }
}
