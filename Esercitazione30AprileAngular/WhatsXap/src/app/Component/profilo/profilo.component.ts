import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProfiloService } from '../../Service/profilo.service';
import { Observable } from 'rxjs';
import { Risposta } from '../../Model/risposta';
import { Utente } from '../../Model/utente';
import { ObjectId } from 'mongodb';
import { Room } from '../../Model/room';
import { AppComponent } from '../../app.component';
import swal from 'sweetalert';

@Component({
  selector: 'app-profilo',
  templateUrl: './profilo.component.html',
  styleUrl: './profilo.component.css',
})
export class ProfiloComponent {
  username: string = JSON.stringify(localStorage.getItem('username')).replace(
    /["]+/g,
    ''
  );
  codice: ObjectId | undefined;
  rooms: Room[] | undefined = new Array();
  tutte: Room[] | undefined = new Array();
  handleInterval: any;

  constructor(
    private router: Router,
    private service: ProfiloService,
    private app: AppComponent
  ) {
    if (!localStorage.getItem('ilToken')) router.navigateByUrl('/login');
  }

  recuperaStanze(): void {}

  ngOnInit(): void {
    this.app.checkToken();
    this.service.recuperaProfilo().subscribe((risultato) => {
      this.username = <string>risultato.data.use;
      this.codice = <ObjectId>risultato.data.codUte;

      this.service
        .recuperaStanzePerUtente(this.username)
        .subscribe((risultato) => {
          this.rooms = risultato.data;
          console.log(this.rooms);
        });

      this.service
        .recuperaTutteLeAltreStanze(this.username)
        .subscribe((risultato) => {
          this.tutte = risultato.data;
        });
    });
  }

  modaleAggiunta(stanza: String | undefined): void {
    swal({
      title: 'Vuoi unirti al gruppo?',
      buttons: true,
    }).then((willDelete) => {
      if (willDelete) {
        this.service
          .aggiungiUtenteAStanza(this.username, <string>stanza)
          .subscribe((risultato) => {
            console.log(risultato);
            this.router.navigateByUrl('chat/' + stanza);
          });
      }
    });
  }
}
