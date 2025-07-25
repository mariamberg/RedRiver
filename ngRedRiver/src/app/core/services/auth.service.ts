import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://redriverbackenddemo-g9bgdehsefcqcmaw.swedencentral-01.azurewebsites.net/api/v1/user';

  private http = inject(HttpClient);

  logIn(username: string, password: string): Observable<string> {
    const body = { username, password };

    return this.http.post(this.apiUrl + '/login', body, { responseType: 'text' }).pipe(

      map(response => response),
      catchError(error => {
        console.error('Login failed', error);
        return throwError(() => new Error('Login failed'));
      })
    );
  }

  register(username: string, password: string): Observable<unknown> {
    const body = { username: username, password: password }; 
  
    return this.http.post(this.apiUrl, body).pipe(
      catchError(error => {
        console.error('Registration failed', error);
        return throwError(() => new Error('Registration failed'));
      })
    );
  }
  

  storeToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  logOut() {
    localStorage.removeItem('authToken');
  }
}
