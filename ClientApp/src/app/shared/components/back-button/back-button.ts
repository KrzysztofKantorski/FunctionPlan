import { Component, inject } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'back-button',
  imports: [MatIconModule],
  templateUrl: './back-button.html'
})
export class BackButton {
  private router = inject(Router);
  private location = inject(Location)
  goBack(): void
  {
    //Previous address saved in history
    if(window.history.length > 1)
    {
      this.location.back()
    }
    else
    {
      this.router.navigate(['/main-page'])
    }
  }
}
