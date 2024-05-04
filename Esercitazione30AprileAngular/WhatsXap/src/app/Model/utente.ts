export class Utente {
  codUte: String | undefined;
  use: String | undefined;
  pas: String | undefined;

  constructor(use: string, pas: string) {
    this.use = use;
    this.pas = pas;
  }
}
