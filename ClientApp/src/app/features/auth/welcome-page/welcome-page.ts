import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import { BoardBackground } from '../../../shared/components/board-background/board-background';
import { ActionButton } from '../../../shared/components/action-button/action-button';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-welcome-page',
  imports: [MatButtonModule, BoardBackground, ActionButton, MainHeader, RouterLink],
  templateUrl: './welcome-page.html'
})
export class WelcomePage {}
