import { Component, inject, Input, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HealthService } from '../../../core/services/health-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ServerErrorAnimation } from '../../../shared/components/server-error-animation/server-error-animation';
@Component({
  selector: 'server-error',
  imports: [ServerErrorAnimation, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './server-error.html'
})
export class ServerError {

  private healthService = inject(HealthService)
  private snackBar = inject(MatSnackBar);

  isChecking = signal<boolean>(false);
  
  retry(){
    if (this.isChecking()) return;

    this.isChecking.set(true);

    //Check if serwer responds
    this.healthService.getHealth().subscribe({
      next: ()=>{
        //Server works again
        this.isChecking.set(false);
        window.location.href = '/main-page';
      },
      error: (err)=>{
        this.isChecking.set(false);
        this.snackBar.open('Server still down. Try again later.', 'OK', {
          duration: 4000,
          panelClass: 'error-snackbar'
        });
      }

    })
  } 
}
