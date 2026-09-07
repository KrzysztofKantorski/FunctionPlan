import { Component } from '@angular/core';
import { BoardBackground } from '../../../shared/components/board-background/board-background';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { ActionButton } from '../../../shared/components/action-button/action-button';
import { FormInput } from '../../../shared/components/form-input/form-input';
import { GoogleBtn } from '../../../shared/components/google-btn/google-btn';
@Component({
  selector: 'app-verify-page',
  imports: [BoardBackground, MainHeader, ActionButton, FormInput, GoogleBtn],
  templateUrl: './verify-page.html'
})
export class VerifyPage {}
