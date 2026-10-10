import { Component, inject } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { UserTableView } from '../../../shared/components/user-table-view/user-table-view';
import { UserService } from '../../../core/services/user-service';
import { AsyncPipe } from '@angular/common';
import { LoadingSpinner } from '../../../shared/components/loading-spinner/loading-spinner';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/back-button/back-button';
@Component({
  selector: 'user-table',
  imports: [MatProgressSpinnerModule, UserTableView, AsyncPipe, LoadingSpinner, Navbar, NavbarBtnGroup, UserMenu, BackButton],
  templateUrl: './user-table.html'
})

export class UserTable {
  private userService = inject(UserService);
  users$ = this.userService.getUsersDetails();
}
