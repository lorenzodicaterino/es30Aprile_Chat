import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Risposta } from './Model/risposta';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  isLoggato: boolean = false;

  constructor(private router: Router, private http: HttpClient) {}

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

  creaGlobal(): Observable<Risposta> {
    return this.http.get<Risposta>('https://localhost:7035/Room/global');
  }

  ngOnInit(): void {
    this.creaGlobal().subscribe((risultato) => {
      console.log(risultato);
    });
  }
}
