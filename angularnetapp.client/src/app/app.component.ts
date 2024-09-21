import { CommonModule } from '@angular/common';
import { Component, AfterViewInit, ViewChild, ElementRef, ChangeDetectorRef, HostListener, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, AbstractControl, ValidatorFn } from '@angular/forms';
import { RegisterMemberService } from './register-member.service';
import { LoginService } from './login.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DatePipe, formatDate } from '@angular/common';
import { ProductService } from './product.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, ReactiveFormsModule],
  providers: [DatePipe, HttpClient],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements AfterViewInit, OnInit, OnDestroy {

  logOff() {
    this.showMainMenu = false;
    this.showModal = true;
  }
  title = 'temp49';
  user = '';
  showMainMenu: boolean = false;
  showModal: boolean = false;
  showModalRegister: boolean = false;
  loginForm: any;
  registerForm: any;
  newItem = {
    email: '',
    password: ''
  };
  newItemRegister = {
    email: '',
    password: '',
    passwordConfirm: ''
  };
  isMainMenuVisible = false;
  isMainMenuVisibleRegister = false;
  statusMessage: string = '';
  currentDate: any;
  currentTime: string | undefined;
  currentDateTime: string | undefined;
  private intervalId: any;
  constructor(private fb: FormBuilder, private router: Router, private cd: ChangeDetectorRef, private registerMemberService: RegisterMemberService, private http: HttpClient, private loginService: LoginService, private datePipe: DatePipe, private productService: ProductService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });

    this.registerForm = this.fb.group({
      emailRegister: ['', [Validators.required, Validators.email]],
      passwordRegister: ['', [Validators.required]],
      passwordConfirm: ['', [Validators.required]],
    }, {
      validator: this.passwordMatchValidator
    })
  }
  ngOnInit(): void {
    const now = new Date();
    this.currentDateTime = formatDate(now, 'MMMM d, y, h:mm:ss a', 'en-US');
    this.updateTime();
    this.intervalId = setInterval(() => {
      this.updateTime();
    }, 1000);
  }


  get email() {
    return this.loginForm?.get('email');
  }

  get password() {
    return this.loginForm?.get('password');
  }

  get emailRegister() {
    return this.loginForm?.get('email');
  }

  get passwordRegister() {
    return this.registerForm?.get('passwordRegister');
  }

  get passwordConfirm() {
    return this.registerForm?.get('passwordConfirmRegister');
  }

  passwordMatchValidator(form: FormGroup) {
    return form?.get('passwordRegister')?.value === form?.get('passwordConfirm')?.value
      ? null
      : { mismatch: true };
  }

  onLogin(email: string, password: string) {
    this.loginService.login(email, password).subscribe({
      next: (result: number) => {
        console.log('Returned number:', result);
      },
      error: (error: any) => {
        console.error('There was an error!', error);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  private updateTime(): void {
    const now = new Date();
    this.currentTime = now.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: true });
  }

  onSubmit(event: Event): void {
    event.preventDefault();
    this.user = this.newItem.email;
    this.user = this.newItemRegister.email;
    this.loginService.login(this.newItem.email, this.newItem.password).subscribe(
      (result: number) => {
        console.log("res: ", result)
        if (result === 1) {
          this.showMainMenu = true;
          this.showModal = false;
          this.user = this.newItem.email;
          console.log('Login successful');
          // Handle success
        } else {
          this.statusMessage = "Invalid Email or Password !"
          this.showMainMenu = false;
          console.log('Login failed');
          // Handle failure
        }
      }
    );




    //const form = event.target as HTMLFormElement;
    //const formData = new FormData(form);
    //const emailLogin01 = formData.get('email')?.toString().trim();
    //const passwordLogin = formData.get('password')?.toString().trim();
    //const data = {
    //  emailLogin: emailLogin,
    //  passwordLogin: passwordLogin
    //}
    //alert(this.newItem.email + " " + this.newItem.password)
    //console.log("result: ", this.loginService.login(this.newItem.email , this.newItem.password))

    //this.onLogin("xxx@gmail.com","111");

    /*
    if (this.loginService.postData(emailLogin, passwordLogin, Observable<any>)){
      this.showMainMenu = true;
      this.showModal = false;
    }
    else {
      this.showMainMenu = false;
      this.showModal = true;
      this.statusMessage = "Invalid email or password !!!"
    } */
  }


  onSubmitRegister(event: Event): void {
    event.preventDefault();
    this.user = this.newItem.email;
    this.user = this.newItemRegister.email;
    const form = event.target as HTMLFormElement;
    const formData = new FormData(form);
    const emailRegister = formData.get('emailRegister');
    const passwordRegister = formData.get('passwordRegister');
    const confirmPasswordRegister = formData.get('passwordConfirmRegister');
    const data = {
      email: emailRegister?.toString().trim(),
      password: passwordRegister?.toString().trim(),
      passwordConfirm: confirmPasswordRegister?.toString().trim()
    }

    //alert(email + " " + password + " " + confirmPassword)

    //alert(formData.get("passwordConfirmRegister"))

    //this.http.post('https://localhost:7129/register', { "id":0, email, password, confirmPassword })
    // .subscribe({
    //  next: response => console.log('Registration successful', response),
    //   error: error => console.error('Registration failed', error)
    // });

    //alert(document.getElementById('loginModal')?.id);
    //const loginModal = document.getElementById('loginModal');
    //const modal = new (window as any).bootstrap.Modal(loginModal);
    //modal.hide();


    this.registerMemberService.addMember(data).subscribe({
      next: (result) => {
        if (result === 1) {
          this.showMainMenu = true;
          this.showModalRegister = false;
          console.log("Member added successfully");
        } else {
          this.showMainMenu = false;
          this.showModalRegister = true;
          this.statusMessage = "Member already exists!"
        }
      },
      error: (error) => {
        console.error("Error occurred while adding member", error);
      }
    });

  }

  openModal() {
    this.showMainMenu = false;
    this.showModal = true;
    this.showModalRegister = false;
  }

  openModalRegister() {

    this.showModal = false;
    this.showModalRegister = true;
    this.showMainMenu = false;
  }

  clearFields() {
    //this.showModal = true;
    this.showModalRegister = true;
    //event.preventDefault()
    //this.registerForm.reset();
    //this.openModalRegister();
    //const inputs = document.querySelectorAll('input');
    //this.openModalRegister();
    //this.openModal();
    //this.showModalRegister = true;
    //const inputs = document.querySelectorAll('input');
  }

  /*
    initializeApp(): void {
      const loginModal = document.getElementById('loginModal');
      if (loginModal) {
        const modal = new (window as any).bootstrap.Modal(loginModal);
        modal.hide();
        modal._element.addEventListener('hidden.bs.modal', () => {
          this.isMainMenuVisible = true;
        });
        modal.show();
      }
    } 
      */

  ngAfterViewInit(): void {
    //const loginModal = document.getElementById('loginModal');
    //const modal = new (window as any).bootstrap.Modal(loginModal);
    //modal.show();

    this.showMainMenu = false;
    this.showModalRegister = false;
    this.showModal = true;
    this.cd.detectChanges(); // Manually trigger change detection
  }

}



