
import { RouterModule, Routes } from '@angular/router';
import { InvoiceFormComponent } from './invoice-form/invoice-form.component';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component'

export const routes: Routes = [
  { path: '', component: InvoiceFormComponent }, // Default route
  { path: 'invoice', component: InvoiceFormComponent },
  { path: '**', redirectTo: '' } // Wildcard route for a 404 page   
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


/*
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
*/
