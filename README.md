# Traccia 30/04

### Consegna entro le 18:00:00 del 04/05/2024

<p>Creare un sistema di gestione di una chat, ogni persona può iscriversi al portale tramite un apposito portale
inserendo Username e Password. Queste informazioni vanno inserite in un DB SQL SERVER, con password cifrata in MD5. 
Una volta iscritto sarà possibile effettuare il login. Al login sarete inseriti in una stanza dove tutti gli utenti potranno interagire e, quindi, scrivere un messaggio che sarà, con una latnza di massimo 500ms, visualizzato a tutti gli altri utenti. La chat verrà salvata all'interno di uno o più document di MongoDB.
Ogni chat è caratterizzata da Titolo e Descrizione (definiti dal creatore).

HARD: Sarà possibile creare delle stanze personalizzate ed aggiungere uno o più utenti (tramite username)

REQUISITO GRAFICO: Il mittente ha il messaggio di chat sempre a destra mentre il ricevente ha i messaggi ricevuti a sinistra. Ogni elemento ha l'orario con nome mittente.

</p>

```sql
CREATE TABLE utente(
utenteID INT PRIMARY KEY IDENTITY(1,1),
codice_utente VARCHAR(250) DEFAULT NEWID(),
username VARCHAR(250) NOT NULL UNIQUE,
passw VARCHAR(250) NOT NULL,
deleted DATETIME DEFAULT NULL
);
```

<video width="320" height="240" controls>
  <source src="WhatsApp Video 2024-05-06 at 13.14.53.mp4" type="video/mp4">
</video>
