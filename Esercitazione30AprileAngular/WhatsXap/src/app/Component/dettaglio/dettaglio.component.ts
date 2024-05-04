import { Component } from '@angular/core';
import { ChatService } from '../../Service/chat.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Room } from '../../Model/room';

@Component({
  selector: 'app-dettaglio',
  templateUrl: './dettaglio.component.html',
  styleUrl: './dettaglio.component.css',
})
export class DettaglioComponent {
  nomeChat: string = '';
  stanzaOgg: Room = new Room();
  nomeUtente: string = JSON.stringify(localStorage.getItem('username')).replace(
    /["]+/g,
    ''
  );
  constructor(
    private service: ChatService,
    private rottaAttiva: ActivatedRoute,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.rottaAttiva.params.subscribe((parametro) => {
      this.nomeChat = parametro['code'];
    });

    this.service
      .dettaglioStanza(<string>this.nomeChat)
      .subscribe((risultato) => {
        this.stanzaOgg = risultato.data;
        console.log(this.stanzaOgg);
      });
  }

  tornaAChat(): void {
    this.route.navigateByUrl('/chat/' + this.nomeChat);
  }

  abbandonaGruppo(nomeUtente: string, nomeChat: string): void {
    nomeChat = <string>this.nomeChat;
    nomeUtente = <string>this.nomeUtente;
    this.service.abbandona(nomeChat, nomeUtente).subscribe((risultato) => {
      console.log(risultato.status);
      this.route.navigateByUrl('profilo');
    });
  }
}
