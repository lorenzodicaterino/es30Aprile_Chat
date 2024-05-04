import { Component } from '@angular/core';
import { Utente } from '../../Model/utente';
import { AuthService } from '../../Service/auth.service';
import { Router } from '@angular/router';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-sing-in',
  templateUrl: './sing-in.component.html',
  styleUrl: './sing-in.component.css',
})
export class SingInComponent {
  username: String | undefined;
  password: String | undefined;

  constructor(
    private service: AuthService,
    private router: Router,
    private app: AppComponent
  ) {}

  salva(): void {
    if (this.username?.length == 0 || this.password?.length == 0) return;

    this.service
      .registrazione(<string>this.username, <string>this.password)
      .subscribe((risultato) => {
        if (risultato.status == 'SUCCESS') {
          this.router.navigateByUrl('/login');
        } else alert('ERRORE');
      });
    this.app.checkToken();
  }
}
