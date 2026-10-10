import { Component, inject, Input } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
import { LoadingSpinner } from '../../../shared/components/loading-spinner/loading-spinner';
import {AsyncPipe} from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs';
import { BackButton } from '../../../shared/components/back-button/back-button';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { UserStatsCard } from '../../../shared/components/user/user-stats-card/user-stats-card';
import { UserAvatar } from '../../../shared/components/meeting/user-avatar/user-avatar';
@Component({
  selector: 'user-details',
  imports: [LoadingSpinner, AsyncPipe,  Navbar, NavbarBtnGroup, UserMenu, BackButton, UserStatsCard, UserAvatar],
  templateUrl: './user-details.html'
})
export class UserDetails {
  private route = inject(ActivatedRoute);
  private userService = inject(UserService);

  //Get user id
  userId$ = this.route.paramMap.pipe(
    map(params => Number(params.get('id')))
  );

  //Get user stats
  stats$ = this.userId$.pipe(
    switchMap(userId => this.userService.getUserStats(userId))
  );
}
