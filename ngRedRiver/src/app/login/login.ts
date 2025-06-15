import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  newUsername: string = '';
  newPassword: string = '';
  registerErrorMessage: string = '';
  registerSuccessMessage: string = '';

  // Using inject() instead of constructor injection
  private authService = inject(AuthService);
  private router = inject(Router);

  logIn() {
    this.authService.logIn(this.username, this.password).subscribe({
      next: token => {
        console.log('login');
        this.authService.storeToken(token);
        this.router.navigate(['/book-list']);
      },
      error: err => {
        console.error('Login failed', err);
        this.errorMessage = 'Invalid username or password.';
      },
    });
  }

  register() {
    this.authService.register(this.newUsername, this.newPassword).subscribe({
      next: () => {
        console.log('User registered successfully');
        this.registerSuccessMessage = 'User registered successfully!';
        this.registerErrorMessage = '';
        this.router.navigate(['/book-list']);
      },
      error: err => {
        console.error('Registration failed', err);
        this.registerErrorMessage = 'Registration failed. Try again.';
        this.registerSuccessMessage = '';
      },
    });
  }
}
