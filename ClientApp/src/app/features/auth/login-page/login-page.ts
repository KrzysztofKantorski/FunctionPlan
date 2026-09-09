import { Component, inject } from '@angular/core';
import { AuthLayout } from '../../../shared/components/auth-layout/auth-layout';
import { GoogleBtn } from '../../../shared/components/google-btn/google-btn';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { ActionButton } from '../../../shared/components/action-button/action-button';
import { FormInput } from '../../../shared/components/form-input/form-input';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../../core/auth/login-service';

@Component({
  selector: 'app-login-page',
  imports: [AuthLayout, GoogleBtn, MainHeader, ActionButton, FormInput, ReactiveFormsModule],
  templateUrl: './login-page.html'
})

export class LoginPage {

  private fb = inject(FormBuilder);
  private loginService = inject(LoginService);
  private router = inject(Router);

  //Form fields
  loginForm = this.fb.nonNullable.group({
    email: ['', 
      [Validators.required, Validators.email]
    ],
    password: ['', 
      Validators.required
    ]
  })

  onSubmit(){

    //Check form validation
    if (this.loginForm.invalid) 
    {
      this.loginForm.markAllAsTouched();
      return;
    }

    //Call service method
    this.loginService.login(this.loginForm.getRawValue()).subscribe({
      next: () => {
        //Navigate to main page
       this.router.navigate(['/dashboard']);
      },
      error: () => {
        //Reset password field
        this.loginForm.controls.password.reset();
      }
    });
  }
}
