import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NewBook } from '../../models/new-book.model';
import { UpdateBook } from '../../models/update-book.model';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [ReactiveFormsModule, HttpClientModule, CommonModule],
  templateUrl: './book-form.html',
  styleUrls: ['./book-form.css'],
})
export class BookForm implements OnInit {
  private fb = inject(FormBuilder);
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  bookForm: FormGroup;
  id?: string;

  constructor() {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      year: ['', Validators.required],
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    console.log('id:', id);
    if (id) {
      this.loadBook(id);
    }
  }

  loadBook(id: string) {
    this.bookService.get(id).subscribe(book => {
      this.bookForm.patchValue({ title: book.title, author: book.author, year: book.year });
      this.id = id;
      this.bookForm.updateValueAndValidity();
      console.log('book:', book);
      console.log('bookForm:', this.bookForm.value);
    });
  }

  submitForm() {
    if (this.id && this.bookForm.valid) {
      const uppdateradBook: UpdateBook = new UpdateBook(
        this.bookForm.value.title,
        this.bookForm.value.author,
        this.bookForm.value.year
      );
      this.bookService.update(this.id, uppdateradBook).subscribe(() => {
        console.log('Booken uppdaterad:', uppdateradBook);
        this.router.navigate(['/book-list']);
      });
    } else if (this.bookForm.valid) {
      const newBook: NewBook = new NewBook(
        this.bookForm.value.title,
        this.bookForm.value.author,
        this.bookForm.value.year
      );
      this.bookService.save(newBook).subscribe(() => {
        this.router.navigate(['/book-list']);
      });
    }
  }
}
