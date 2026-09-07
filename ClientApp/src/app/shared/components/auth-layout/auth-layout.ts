import { Component, Input } from '@angular/core';
@Component({
  selector: 'auth-layout',
  standalone: true,
  templateUrl: './auth-layout.html',
  styleUrl: './auth-layout.scss',
})
export class AuthLayout {
  @Input() imagePath: string = 'auth-bg-dark.png';
}
