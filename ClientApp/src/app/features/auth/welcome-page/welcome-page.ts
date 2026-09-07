import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import { BoardBackground } from '../../../shared/components/board-background/board-background';
import { ActionButton } from '../../../shared/components/action-button/action-button';
import { MainHeader } from '../../../shared/components/main-header/main-header';
@Component({
  selector: 'app-welcome-page',
  imports: [MatButtonModule, BoardBackground, ActionButton, MainHeader],
  templateUrl: './welcome-page.html'
})
export class WelcomePage {}
