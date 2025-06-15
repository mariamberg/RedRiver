import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewQuote } from '../models/new-quote.model';
import { Quote } from '../models/quote.model';
import { UpdateQuote } from '../models/update-quote.models';

@Injectable({ providedIn: 'root' })
export class QuoteService {
  private http = inject(HttpClient);
  private apiUrl = 'https://redriverbackenddemo-g9bgdehsefcqcmaw.swedencentral-01.azurewebsites.net/api/v1/quote';
  private apiAllUrl = this.apiUrl + '/all';
  private quote: Quote[] = [];

  all(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.apiAllUrl);
  }

  get(id: string): Observable<Quote> {
    return this.http.get<Quote>(`${this.apiUrl}/${id}`);
  }

  save(newQuote: NewQuote): Observable<Quote> {
    return this.http.post<Quote>(this.apiUrl, newQuote);
  }

  update(id: string, uppdateradQuote: UpdateQuote): Observable<Quote> {
    return this.http.put<Quote>(`${this.apiUrl}/${id}`, uppdateradQuote);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
