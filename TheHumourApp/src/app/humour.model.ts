
export class Humour {
  joke!: string;
  id!: number;
  constructor(id: number, joke: string){
    this.id = id;
    this.joke = joke;
  }
}
