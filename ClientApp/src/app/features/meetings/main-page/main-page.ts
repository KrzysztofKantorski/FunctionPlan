import { Component, inject } from '@angular/core';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { UserService } from '../../../core/services/user-service';
import { UserProfile } from '../../../core/models/user-model';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
@Component({
  selector: 'app-main-page',
  imports: [MainHeader, AsyncPipe],
  templateUrl: './main-page.html'
})
export class MainPage {

  private userService = inject(UserService);
  userProfile$: Observable<UserProfile> | undefined;

  ngOnInit(): void
  {
    this.userProfile$ = this.userService.getUserDetails();
  }
}
