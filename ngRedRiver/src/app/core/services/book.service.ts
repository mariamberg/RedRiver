import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book.model';
import { NewBook } from '../models/new-book.model';
import { UpdateBook } from '../models/update-book.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5226/api/v1/book';
  private apiAllUrl = 'http://localhost:5226/api/v1/book/all';

  private books: Book[] = [];

  all(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiAllUrl);
  }

  get(id: string): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  save(newBook: NewBook): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, newBook);
  }

  update(id: string, updateBook: UpdateBook): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${id}`, updateBook);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
