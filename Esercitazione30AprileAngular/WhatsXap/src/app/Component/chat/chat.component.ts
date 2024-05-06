import { MessaggioService } from './../../Service/messaggio.service';
import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../Service/chat.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Messaggio } from '../../Model/messaggio';
import { Risposta } from '../../Model/risposta';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  constructor(
    private rottaAttiva: ActivatedRoute,
    private router: Router,
    private service: ChatService,
    private messaggioService: MessaggioService
  ) {}

  messaggi: Messaggio[] = new Array();
  nomeChat: string | undefined;
  nomeUtente: string = JSON.stringify(localStorage.getItem('username')).replace(
    /["]+/g,
    ''
  );
  handleInterval: any;
  messaggioInput: string | undefined;

  backToProfilo(): void {
    this.router.navigateByUrl('/profilo');
  }

  stampaMessaggi(nomeChat: string): void {
    this.service.stampaMessaggi(nomeChat).subscribe((risultato) => {
      this.messaggi = risultato.data;
    });
    setTimeout(() => {
      window.scrollTo(0, document.body.scrollHeight);
    }, 50);
  }

  ngOnInit(): void {
    setTimeout(() => {
      window.scrollTo(0, document.body.scrollHeight);
    }, 50);
    document.body.className = 'selector';
    this.rottaAttiva.params.subscribe((parametro) => {
      this.nomeChat = parametro['code'];
    });
    this.handleInterval = setInterval(() => {
      this.stampaMessaggi(<string>this.nomeChat);
    }, 50);

    this.stampaMessaggi(<string>this.nomeChat);
  }

  inviaMessaggioComponent(): void {
    this.messaggioService
      .invio(
        <string>this.messaggioInput,
        this.nomeUtente,
        <string>this.nomeChat
      )
      .subscribe((risultato) => {
        console.log(risultato.status);
        window.scrollTo(0, document.body.scrollHeight);
      });
  }

  ngOnDestroy(): void {
    document.body.className = '';
    clearInterval(this.handleInterval);
  }
}
