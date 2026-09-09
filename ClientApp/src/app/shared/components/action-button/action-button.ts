import { Component, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'action-button',
  imports: [MatButtonModule],
  templateUrl: './action-button.html',
  styleUrl: './action-button.scss',
})
export class ActionButton {
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
}
