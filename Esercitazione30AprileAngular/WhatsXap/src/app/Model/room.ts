import { ObjectId } from 'mongodb';

export class Room {
  nom: String | undefined;
  des: String | undefined;
  dat: Date | undefined;
  cre: String | undefined;
  par: String[] | undefined;
  mes: ObjectId[] | undefined;
}
