import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSlideToggle} from '@angular/material/slide-toggle';

import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { AuthService } from './core/auth/auth-service';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatSlideToggle, MatCardModule, MatButtonModule],
  templateUrl: './app.html'
})
export class App {
  protected readonly title = signal('ClientApp');
  private authService = inject(AuthService)

  ngOnInit(): void {
    if (this.authService.hasToken()) {
      this.authService.fetchCurrentUser().subscribe({
        //Logout if token expired or is incorrect
        error: () => this.authService.logout() 
      });
    }
  }
}
