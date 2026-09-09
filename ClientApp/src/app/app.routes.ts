import { Routes } from '@angular/router';
import { WelcomePage } from './features/auth/welcome-page/welcome-page';
import {LoginPage} from './features/auth/login-page/login-page';
import {RegisterPage} from './features/auth/register-page/register-page';
import {VerifyPage} from './features/auth/verify-page/verify-page';
import {MainPage} from './features/meetings/main-page/main-page';
import { guestGuard } from './core/guards/guest-guard';

export const routes: Routes = [
    {
        path :"", 
        component: WelcomePage,
        canActivate: [guestGuard]
    },
    {
        path :"login",
        component: LoginPage,
        canActivate: [guestGuard]
    },
    {
        path :"register", 
        component: RegisterPage,
        canActivate: [guestGuard]
    },
    {
        path :"verify", 
        component: VerifyPage,
        canActivate: [guestGuard]
    },
    {
        path:"main-page", 
        component: MainPage
    }
];
