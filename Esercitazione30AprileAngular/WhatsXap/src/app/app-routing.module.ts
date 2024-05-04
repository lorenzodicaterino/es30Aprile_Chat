import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Component/login/login.component';
import { ProfiloComponent } from './Component/profilo/profilo.component';
import { SingInComponent } from './Component/sing-in/sing-in.component';
import { ChatComponent } from './Component/chat/chat.component';
import { DettaglioComponent } from './Component/dettaglio/dettaglio.component';
import { CreateChatComponent } from './Component/create-chat/create-chat.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'registrati', component: SingInComponent },
  { path: 'profilo', component: ProfiloComponent },
  { path: 'chat/:code', component: ChatComponent },
  { path: 'dettaglio/:code', component: DettaglioComponent },
  { path: 'create', component: CreateChatComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
