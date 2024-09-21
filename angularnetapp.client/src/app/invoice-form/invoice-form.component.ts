import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ProductService } from '../product.service';
import { catchError, finalize, map } from 'rxjs/operators';
import { firstValueFrom, Observable, of } from 'rxjs';
import { DateTime } from 'luxon';


@Component({
  selector: 'app-invoice-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  providers: [HttpClient],
  templateUrl: './invoice-form.component.html',
  styleUrls: ['./invoice-form.component.css']
})
export class InvoiceFormComponent {
  [x: string]: any;
  invoiceForm: FormGroup;
  products: any[] = [];
  onFailedSubmitMsg = "";
  isDuplicate: boolean = false;
  isInvoiceNoAdded: boolean = false;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private productService: ProductService
  ) {
    this.invoiceForm = this.fb.group({
      customer: ['', Validators.required],
      invoiceNo: ['', Validators.required],
      invoiceDate: ['', Validators.required],
      items: this.fb.array([this.createItem()]),
      grandTotal: [{ value: 0, disabled: true }]
    });

    this.loadProducts();
  }

  createItem(): FormGroup {
    return this.fb.group({
      product: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [{ value: 0, disabled: true }, [Validators.required, Validators.min(0)]],
      lineTotal: [{ value: 0, disabled: true }]
    });
  }

  get items() {
    return this.invoiceForm.get('items') as FormArray;
  }

  addItem(): void {
    this.items.push(this.createItem());
  }

  removeItem(index: number): void {
    if (this.items.length > 1) {
      this.items.removeAt(index);
      this.calculateTotals();
    }
  }

  calculateTotals(): void {
    let grandTotal = 0;
    this.items.controls.forEach(item => {
      const quantity = item.get('quantity')?.value || 0;
      const unitPrice = item.get('unitPrice')?.value || 0;
      const lineTotal = quantity * unitPrice;
      item.get('lineTotal')?.setValue(lineTotal, { emitEvent: false });
      grandTotal += lineTotal;
    });
    this.invoiceForm.get('grandTotal')?.setValue(grandTotal);
  }

  onProductChange(index: number): void {
    const selectedProduct = this.items.at(index).get('product')?.value;
    const product = this.products.find(p => p.id === Number(selectedProduct));
    if (product) {
      this.items.at(index).get('unitPrice')?.setValue(product.price);
      this.calculateTotals();
    }
  }

  loadProducts(): void {
    this.productService.loadInvoices().subscribe(products => {
      this.products = products;
    });
  }

  onSubmit(event: Event): void {
    event.preventDefault();

    if (this.invoiceForm.invalid) {
      this.invoiceForm.markAllAsTouched();
      return;
    }

    this.onFailedSubmitMsg = "";
    var customer = this.invoiceForm.get('customer')?.value || '';
    var invoiceNo = this.invoiceForm.get('invoiceNo')?.value || '';
    var invoiceDate = this.invoiceForm.get('invoiceDate')?.value || '';

    const formattedDate = DateTime.fromJSDate(new Date(invoiceDate)).toFormat('yyyy-MM-dd');

    this.productService.isInvoiceNoDuplicate(invoiceNo).subscribe((response: boolean) => {
      this.isDuplicate = response;
      if (this.isDuplicate) {
        this.onFailedSubmitMsg = "Invoice no. already exists!!!";
      } else {
        const invoiceOrders = this.items.controls.map(item => ({
          productid: item.get('product')?.value,
          quantity: item.get('quantity')?.value,
          price: item.get('unitPrice')?.value
        }));

        const data = {
          id: 0,
          customerName: customer,
          invoiceNo: invoiceNo,
          invoiceDate: formattedDate,
          invoiceorders: invoiceOrders
        };

        this.productService.addInvoice(data).subscribe(() => {
          this.onFailedSubmitMsg = "The transaction is successful!!!";
          this.onReset();
        });
      }
    });
  }

  onReset(): void {
    this.invoiceForm.reset();
    this.items.clear();
    this.addItem();
  }
}
