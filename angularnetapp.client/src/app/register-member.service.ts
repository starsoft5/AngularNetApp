import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import Environment from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterMemberService {

  private baseApiUrl = Environment.baseApi_url;

  private apiUrl = `${this.baseApiUrl}/Register`;

  constructor(private http: HttpClient) { }

  addMember(data: any): Observable<number> {
    const dataSubmitted = { "email": data.email, "password": data.password, "confirmPassword": data.passwordConfirm };
    return this.http.post<number>(this.apiUrl, dataSubmitted).pipe(
      map(response => {
        // Assuming the response will be 1 or -1, modify this as needed based on your actual response structure.
        return response;
      }),
      catchError(error => {
        console.error(error);
        // Return -1 in case of an error or any other logic you want
        return of(-1);
      })
    );
  }



}
