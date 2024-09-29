import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import Environment from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseApiUrl = Environment.baseApi_url;

  private apiUrl = `${this.baseApiUrl}/product`;

  constructor(private http: HttpClient) { }

  addInvoice(data: any): Observable<any> {
    console.log('productService Date: ', data.invoiceDate)
    return this.http.post(this.apiUrl, data);
  }

  loadInvoices(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  isInvoiceNoDuplicate(invoiceNo: string): Observable<boolean> {
    const url = `${this.apiUrl}/IsInvoiceNoDuplicate?invoiceNo=${invoiceNo}`;
    return this.http.get<boolean>(url);
  }

}
