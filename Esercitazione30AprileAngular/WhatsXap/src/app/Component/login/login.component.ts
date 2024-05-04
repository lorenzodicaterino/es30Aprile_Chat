import { AuthService } from './../../Service/auth.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  user: string = '';
  pass: string = '';

  constructor(
    private service: AuthService,
    private router: Router,
    private app: AppComponent
  ) {}

  verifica(): void {
    this.app.checkToken();
    this.service.login(this.user, this.pass).subscribe((risultato) => {
      if (risultato.token) {
        localStorage.setItem('ilToken', risultato.token);
        localStorage.setItem('username', this.user);
        this.router.navigateByUrl('/profilo');
      } else alert('ERRORE');
    });
  }

  ngOnInit(): void {
    this.app.checkToken();
    localStorage.setItem('ilToken', '');
    localStorage.setItem('username', '');
    this.router.navigate([this.router.url]);
  }
}
