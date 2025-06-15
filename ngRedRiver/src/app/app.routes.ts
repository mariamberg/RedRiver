import { Routes } from '@angular/router';
import { BookForm } from './core/features/book-form/book-form';
import { BookList } from './core/features/book-list/book-list';
import { QuoteForm } from './core/features/quote-form/quote-form';
import { QuoteListComponent } from './core/features/quote-list/quote-list';
import { Login } from './login/login';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'book-form', component: BookForm },
  { path: 'book-form/:id', component: BookForm },
  { path: 'book-list', component: BookList },
  { path: 'quote-form', component: QuoteForm },
  { path: 'quote-form/:id', component: QuoteForm },

  { path: 'quote-list', component: QuoteListComponent },
  { path: '**', redirectTo: 'login' },
];
