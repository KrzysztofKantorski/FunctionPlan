import { Component, Input } from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
@Component({
  selector: 'user-menu',
  imports: [MatMenuModule, MatButtonModule],
  templateUrl: './user-menu.html'
})
export class UserMenu {
  @Input() imagePath: string = 'auth-bg-dark.png';
}
