import { CreateChatComponent } from './Component/create-chat/create-chat.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Component/login/login.component';
import { ProfiloComponent } from './Component/profilo/profilo.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { SingInComponent } from './Component/sing-in/sing-in.component';
import { ChatComponent } from './Component/chat/chat.component';
import { DettaglioComponent } from './Component/dettaglio/dettaglio.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProfiloComponent,
    SingInComponent,
    ChatComponent,
    DettaglioComponent,
    CreateChatComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
