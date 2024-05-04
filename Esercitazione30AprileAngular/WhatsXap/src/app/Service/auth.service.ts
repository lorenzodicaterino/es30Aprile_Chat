import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RisToken } from '../Model/ris-token';
import { Utente } from '../Model/utente';
import { Risposta } from '../Model/risposta';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string): Observable<RisToken> {
    let headerCustom = new HttpHeaders();
    headerCustom.set('Content-Type', 'application/json');

    let u: Utente = new Utente(username, password);

    let invio = u;
    console.log(invio);

    return this.http.post<any>('https://localhost:7035/Utente/login', invio, {
      headers: headerCustom,
    });
  }

  registrazione(username: string, password: string): Observable<Risposta> {
    let headerCustom = new HttpHeaders();
    headerCustom.set('Content-Type', 'application/json');

    let u: Utente = new Utente(username, password);

    return this.http.post<any>('https://localhost:7035/Utente/register', u, {
      headers: headerCustom,
    });
  }

  logout(): void {
    localStorage.setItem('IlToken', '');
    localStorage.setItem('username', '');
    this.router.navigateByUrl('/');
  }
}
