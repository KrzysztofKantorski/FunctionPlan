import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';


//Components
import { BoardBackground } from '../../../shared/components/board-background/board-background';
import { ActionButton } from '../../../shared/components/action-button/action-button';
import {GoogleBtn} from '../../../shared/components/google-btn/google-btn';
import { MainHeader } from '../../../shared/components/main-header/main-header';


@Component({
  selector: 'app-welcome-page',
  imports: [ BoardBackground, ActionButton, MainHeader, RouterLink, GoogleBtn ],
  templateUrl: './welcome-page.html'
})
export class WelcomePage {}
