import { Component, inject, Input } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
import { LoadingSpinner } from '../../../shared/components/loading-spinner/loading-spinner';
import {AsyncPipe} from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs';
@Component({
  selector: 'user-details',
  imports: [LoadingSpinner, AsyncPipe],
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
