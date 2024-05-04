import { Room } from './../Model/room';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Risposta } from '../Model/risposta';
import { ServerMonitoringMode } from 'mongodb';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(
    private rottaAttiva: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  risultatoJwt: any;

  stampaMessaggi(nomeChat: string): Observable<Risposta> {
    return this.http.get<Risposta>(
      'https://localhost:7035/Messaggio/' + nomeChat
    );
  }

  dettaglioStanza(nomeChat: string): Observable<Risposta> {
    return this.http.get<Risposta>(
      'https://localhost:7035/Room/dettaglio/' + nomeChat
    );
  }

  abbandona(nomeChat: string, nomeUtente: string): Observable<Risposta> {
    return this.http.get<Risposta>(
      'https://localhost:7035/Room/abbandona/' + nomeChat + '/' + nomeUtente
    );
  }

  creaChat(
    nomeChat: string,
    descrizioneChat: string,
    creatore: string
  ): Observable<Risposta> {
    let r: Room = new Room();
    r.nom = nomeChat;
    r.des = descrizioneChat;
    r.cre = creatore;

    let headerCustom = new HttpHeaders();
    headerCustom.set('Content-Type', 'application/json');

    return this.http.post<Risposta>('https://localhost:7035/Room', r, {
      headers: headerCustom,
    });
  }
}
