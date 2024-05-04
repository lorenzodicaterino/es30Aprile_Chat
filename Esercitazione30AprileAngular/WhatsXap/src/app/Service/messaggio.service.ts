import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Risposta } from '../Model/risposta';
import { Messaggio } from '../Model/messaggio';

@Injectable({
  providedIn: 'root',
})
export class MessaggioService {
  constructor(private http: HttpClient) {}

  invio(
    contenuto: string,
    mittente: string,
    stanza: string
  ): Observable<Risposta> {
    let m: Messaggio = new Messaggio();
    m.nomUte = mittente;
    m.sta = stanza;
    m.con = contenuto;

    let headerCustom = new HttpHeaders();
    headerCustom.set('Content-Type', 'application/json');

    return this.http.post<Risposta>('https://localhost:7035/Messaggio', m, {
      headers: headerCustom,
    });
  }
}
