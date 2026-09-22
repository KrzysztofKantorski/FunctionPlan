import { Component, inject, Input } from '@angular/core';
import { CommentHeader } from '../../../shared/components/comment/comment-header/comment-header';
import { UserAvatar } from '../../../shared/components/meeting/user-avatar/user-avatar';
import { CommentTree } from '../../../shared/components/comment/comment-tree/comment-tree';
import { CommentService } from '../../../core/services/comment-service';
import { BehaviorSubject, Observable, switchMap, tap } from 'rxjs';
import { AuthService } from '../../../core/auth/auth-service';
import { AsyncPipe } from '@angular/common';
import { CurrentUser } from '../../../core/models/user/currentUser';
import { MatDialog } from '@angular/material/dialog';
import { UserDialog } from '../../../shared/components/meeting/user-dialog/user-dialog';
import { CommentMessage } from '../../../core/models/comment/comment';
import { EmptyComments } from '../../../shared/components/comment/empty-comments/empty-comments';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'comment-section',
  imports: [CommentHeader, AsyncPipe, UserAvatar, DatePipe, CommentTree, EmptyComments],
  templateUrl: './comment-section.html'
})
export class CommentSection {
  private dialog = inject(MatDialog);
  private authService = inject(AuthService);
  private commentService = inject(CommentService);

  @Input({ required: true }) meetingId!: number;

  //Get current user info
  currentUser$ = this.authService.currentUserSubject$;

  private refreshSubject = new BehaviorSubject<void>(undefined);

  comments$!: Observable<CommentMessage[]>;

  


  ngOnInit(): void{

    this.comments$ = this.refreshSubject.pipe(
      switchMap(() => this.commentService.getComments(this.meetingId)),
      tap(comments => console.log('Pobrane komentarze:', comments))
      
    );
  }


  openParticipantDetails(user: CurrentUser): void 
  {
    this.dialog.open(UserDialog, 
    {
      data: user,
      width: '320px'
    });
  }
}
