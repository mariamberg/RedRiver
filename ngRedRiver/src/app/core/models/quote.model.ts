export class Quote {
  id: string;
  text: string;
  author: string;

  constructor(id: string, text: string, author: string) {
    this.id = id;
    this.text = text;
    this.author = author;
  }
}
