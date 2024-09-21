import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7207/product';
  restApiIsInvoiceNoDuplicate = "https://localhost:7207/product/IsInvoiceNoDuplicate?invoiceNo="

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
