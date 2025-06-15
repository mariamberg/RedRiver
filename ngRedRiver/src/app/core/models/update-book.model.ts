export class UpdateBook {
  Title: string;
  Author: string;
  Year: number;

  constructor(Title: string, Author: string, Year: number) {
    this.Title = Title;
    this.Author = Author;
    this.Year = Year;
  }
}
