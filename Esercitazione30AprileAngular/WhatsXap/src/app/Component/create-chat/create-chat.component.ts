import { Component } from '@angular/core';
import { ChatService } from '../../Service/chat.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-chat',
  templateUrl: './create-chat.component.html',
  styleUrl: './create-chat.component.css',
})
export class CreateChatComponent {
  nomeUtente: string = JSON.stringify(localStorage.getItem('username')).replace(
    /["]+/g,
    ''
  );

  constructor(private service: ChatService, private route: Router) {}

  nomeChat: string | undefined;
  descrizioneChat: string | undefined;
  creaChat(): void {
    this.service
      .creaChat(
        <string>this.nomeChat,
        <string>this.descrizioneChat,
        this.nomeUtente
      )
      .subscribe((risultato) => {
        console.log(risultato.status);
        this.route.navigateByUrl('/profilo');
      });
  }
}
