import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
@Component({
  selector: 'navbar-btn-group',
  imports: [MatButtonModule, MatMenuModule],
  templateUrl: './navbar-btn-group.html'
})

export class NavbarBtnGroup {}
