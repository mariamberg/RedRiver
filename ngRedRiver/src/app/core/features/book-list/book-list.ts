import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Book } from '../../models/book.model';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.html',
  styleUrls: ['./book-list.css'],
  imports: [CommonModule],
})
export class BookList implements OnInit {
  private router = inject(Router);

  books: Book[] = [];

  private bookService = inject(BookService);
  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.all().subscribe((data: Book[]) => {
      this.books = data;
    });
  }

  addNewBook() {
    this.router.navigate(['/book-form']);
  }

  toCitatList() {
    this.router.navigate(['/quote-list']);
  }

  get(id: string): Observable<Book> {
    return this.bookService.get(id);
  }

  update(id: string) {
    this.router.navigate(['/book-form/', id]);

  }

  delete(id: string) {
    this.bookService.delete(id).subscribe(() => {
      this.loadBooks();
    });
  }
}
