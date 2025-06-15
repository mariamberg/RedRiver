import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NewQuote } from '../../models/new-quote.model';
import { UpdateQuote } from '../../models/update-quote.models';
import { QuoteService } from '../../services/quote.service';

@Component({
  selector: 'app-quote-form',
  standalone: true,
  imports: [ReactiveFormsModule, HttpClientModule, CommonModule],
  templateUrl: './quote-form.html',
  styleUrls: ['./quote-form.css'],
})
export class QuoteForm implements OnInit {
  private fb = inject(FormBuilder);
  private quoteService = inject(QuoteService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  quoteForm: FormGroup;
  id?: string; 

  constructor() {
    this.quoteForm = this.fb.group({
      text: ['', Validators.required],
      author: ['', Validators.required],
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    console.log('id:', id);
    if (id) {
      this.loadQuote(id);
    }
  }

  loadQuote(id: string) {
    this.quoteService.get(id).subscribe(quote => {
      this.quoteForm.patchValue({
        text: quote.text,
        author: quote.author,
      });
      this.id = id;
      this.quoteForm.updateValueAndValidity();
      console.log('quote:', quote);
      console.log('quoteForm:', this.quoteForm.value);
    });
  }

  submitForm() {
    if (this.id && this.quoteForm.valid) {
      const updatedQuote: UpdateQuote = new UpdateQuote(
        this.quoteForm.value.text,
        this.quoteForm.value.author
      );
      this.quoteService.update(this.id, updatedQuote).subscribe(() => {
        console.log('Citatet updated:', updatedQuote);
        this.router.navigate(['/quote-list']);
      });
    } else if (this.quoteForm.valid) {
      const nyttQuote: NewQuote = new NewQuote(
        this.quoteForm.value.quote,
        this.quoteForm.value.author
      );
      console.log(nyttQuote);
      this.quoteService.save(nyttQuote).subscribe(() => {
        this.router.navigate(['/quote-list']);
      });
    }
  }
}
