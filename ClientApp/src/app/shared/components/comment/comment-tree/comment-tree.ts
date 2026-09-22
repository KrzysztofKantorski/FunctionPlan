import { Component, inject, Input } from '@angular/core';
import { CommentMessage } from '../../../../core/models/comment/comment';
import { DatePipe } from '@angular/common';
import { UserAvatar } from '../../meeting/user-avatar/user-avatar';
import { UserDialog } from '../../meeting/user-dialog/user-dialog';
import { MatDialog } from '@angular/material/dialog';
import { ReplyButton } from '../reply-button/reply-button';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'comment-tree',
  imports: [DatePipe, UserAvatar, ReplyButton, CommentTree, MatIconModule],
  templateUrl: './comment-tree.html'
})
export class CommentTree {
  private dialog = inject(MatDialog);
  @Input({ required: true }) comment!: CommentMessage;


  openParticipantDetails(userId: number, username: string): void 
  {
    this.dialog.open(UserDialog, 
    {
      data: {id: userId, username: username},
      width: '320px'
    });
  }

}
