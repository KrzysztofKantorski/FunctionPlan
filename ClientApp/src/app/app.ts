import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSlideToggle} from '@angular/material/slide-toggle';

import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatSlideToggle, MatCardModule, MatButtonModule],
  templateUrl: './app.html'
})
export class App {
  protected readonly title = signal('ClientApp');
}
