import { Component, inject } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
@Component({
  selector: 'back-button',
  imports: [MatIconModule],
  templateUrl: './back-button.html'
})
export class BackButton {
  private router = inject(Router);

  goToMeetings(){
    this.router.navigate(['/main-page']);
  }
}
