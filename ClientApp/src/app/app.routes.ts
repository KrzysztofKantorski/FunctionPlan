import { Routes } from '@angular/router';

import { WelcomePage } from './features/auth/welcome-page/welcome-page';
import {LoginPage} from './features/auth/login-page/login-page';
import {RegisterPage} from './features/auth/register-page/register-page';
import {VerifyPage} from './features/auth/verify-page/verify-page';
import {MainPage} from './features/meetings/main-page/main-page';
import { MeetingInfo } from './features/meetings/meeting-info/meeting-info';
import { MeetingHistory } from './features/meetings/meeting-history/meeting-history';
import { MeetingOrganized } from './features/meetings/meeting-organized/meeting-organized';
import { MeetingAccepted } from './features/meetings/meeting-accepted/meeting-accepted';

import { guestGuard } from './core/guards/guest-guard';
import { authGuard } from './core/guards/auth-guard';
import { ImageGallery } from './shared/components/media-gallery/image-gallery/image-gallery';



export const routes: Routes = [
    {
        path :"", 
        component: WelcomePage,
        canActivate: [guestGuard]
    },
    {
        path: 'server-error',
        loadComponent: () => import('./features/server-error/server-error/server-error').then(m => m.ServerError)
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
        component: MainPage,
        canActivate: [authGuard]
    },
    {
        path:"meeting-info/:id", 
        component: MeetingInfo,
        canActivate: [authGuard]
    },
    {
        path:"meeting-history", 
        component: MeetingHistory,
        canActivate: [authGuard]
    },
    {
        path:"meeting-organized", 
        component: MeetingOrganized,
        canActivate: [authGuard]
    },
    {
        path:"meeting-accepted", 
        component: MeetingAccepted,
        canActivate: [authGuard]
    },
    {
        path:"meeting-info/:id/media", 
        component: ImageGallery,
        canActivate: [authGuard]
    }
];
