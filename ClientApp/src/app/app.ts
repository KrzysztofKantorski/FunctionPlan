import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSlideToggle} from '@angular/material/slide-toggle';

import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { AuthService } from './core/auth/auth-service';
import { switchMap } from 'rxjs';
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
        error: (err) => 
          {
            //Try refreshing access token only if serwer returned 401
            if (err.status === 401) {
              this.tryRefreshToken();
            }
          } 
      });
    }
    else{
      //Access token was not found - try refresh it
      this.tryRefreshToken();
    }
  }


  private tryRefreshToken(): void {
    this.authService.refreshToken().pipe(
      switchMap(() => this.authService.fetchCurrentUser())
    ).subscribe({
      next: (user) => {
        console.log('Session restored:', user);
      },
      error: () => {
        //Httponly cookie not found
        this.authService.logout();
      }
    });
  }
}
