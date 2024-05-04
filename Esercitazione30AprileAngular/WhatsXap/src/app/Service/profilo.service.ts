import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Risposta } from '../Model/risposta';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';

@Injectable({
  providedIn: 'root',
})
export class ProfiloService {
  constructor(private http: HttpClient) {}

  recuperaProfilo(): Observable<Risposta> {
    let contenutoToken = localStorage.getItem('ilToken');
    let nomeUtente: string = JSON.stringify(
      localStorage.getItem('username')
    ).replace(/["]+/g, '');
    let headerCustom = new HttpHeaders({
      Authorization: `Bearer ${contenutoToken}`,
    });

    return this.http.get<Risposta>(
      'https://localhost:7035/Utente/profiloutente',
      {
        headers: headerCustom,
      }
    );
  }

  recuperaStanzePerUtente(use: String): Observable<Risposta> {
    return this.http.get<Risposta>('https://localhost:7035/Room/' + use);
  }

  recuperaTutteLeAltreStanze(use: string): Observable<Risposta> {
    return this.http.get<Risposta>('https://localhost:7035/Room/non/' + use);
  }

  aggiungiUtenteAStanza(use: string, sta: string): Observable<Risposta> {
    return this.http.get<Risposta>(
      'https://localhost:7035/Room/aggiungi_partecipante/' + sta + '/' + use
    );
  }
}
