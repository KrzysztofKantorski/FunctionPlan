import { Component, inject } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import { Router } from '@angular/router';
@Component({
  selector: 'navbar-btn-group',
  imports: [MatButtonModule, MatMenuModule],
  templateUrl: './navbar-btn-group.html'
})

export class NavbarBtnGroup {
  private router = inject(Router);

  goToHistory()
  {
    this.router.navigate(['/meeting-history'])
  }

  goToMain(){
    this.router.navigate(['/main-page'])
  }

  goToOrganized(){
    this.router.navigate(['/meeting-organized'])
  }
}
