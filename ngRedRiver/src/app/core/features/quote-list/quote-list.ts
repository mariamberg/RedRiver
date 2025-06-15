import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Quote } from '../../models/quote.model';
import { QuoteService } from '../../services/quote.service';

@Component({
  selector: 'app-quote-list',
  templateUrl: './quote-list.html',
  styleUrls: ['./quote-list.css'],
  imports: [CommonModule],
})
export class QuoteListComponent implements OnInit {
  private router = inject(Router);

  quotes: Quote[] = [];

  private quoteService = inject(QuoteService);
  ngOnInit() {
    this.loadQuote();
  }

  loadQuote() {
    this.quoteService.all().subscribe((data: Quote[]) => {
      this.quotes = data;
      console.log('Loaded quotes:', data);
    });
  }

  addNewQuote() {
    this.router.navigate(['/quote-form']);
  }

  toBookList() {
    this.router.navigate(['/book-list']);
  }

  save() {
    this.router.navigate(['/quote-form']);
  }

  get(id: string): Observable<Quote> {
    return this.quoteService.get(id);
  }

  update(id: string) {
    this.router.navigate(['/quote-form', id]);
  }

  delete(id: string) {
    this.quoteService.delete(id).subscribe(() => {
      this.loadQuote();
    });
  }
}
