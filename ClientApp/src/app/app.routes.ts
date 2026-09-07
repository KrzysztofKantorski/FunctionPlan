import { Routes } from '@angular/router';
import { WelcomePage } from './features/auth/welcome-page/welcome-page';
import {LoginPage} from './features/auth/login-page/login-page';
import {RegisterPage} from './features/auth/register-page/register-page';
import {VerifyPage} from './features/auth/verify-page/verify-page';
import {MainPage} from './features/meetings/main-page/main-page';

export const routes: Routes = [
    {path :"", component: WelcomePage},
    {path :"login", component: LoginPage},
    {path :"register", component: RegisterPage},
    {path :"verify", component: VerifyPage},
    {path:"main-page", component: MainPage}
];
