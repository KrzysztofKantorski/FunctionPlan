import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
@Component({
  selector: 'loading-spinner',
  imports: [MatProgressSpinnerModule],
  templateUrl: './loading-spinner.html'
})
export class LoadingSpinner {}
