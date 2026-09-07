import { Component } from '@angular/core';
import { AuthLayout } from '../../../shared/components/auth-layout/auth-layout';
import { GoogleBtn } from '../../../shared/components/google-btn/google-btn';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { ActionButton } from '../../../shared/components/action-button/action-button';
@Component({
  selector: 'app-login-page',
  imports: [AuthLayout, GoogleBtn, MainHeader, ActionButton],
  templateUrl: './login-page.html'
})
export class LoginPage {}
