import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, of } from 'rxjs';
import Environment from '../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private baseApiUrl = Environment.baseApi_url;

  private apiUrl = `${this.baseApiUrl}/login`;


  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<number> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const dataSubmitted = { email, password };

    return this.http.post<number>(this.apiUrl, dataSubmitted, { headers }).pipe(
      map((response: number) => {
        return response === 1 ? 1 : -1; // Adjust this to match the structure of your response
      }),
      catchError(() => {
        return of(-1); // Return -1 if there is an error
      })
    );
  }

}
