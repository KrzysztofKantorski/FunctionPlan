import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { CommentMessage } from '../../../../core/models/comment/comment';
import { DatePipe } from '@angular/common';
import { UserAvatar } from '../../meeting/user-avatar/user-avatar';
import { UserDialog } from '../../meeting/user-dialog/user-dialog';
import { MatDialog } from '@angular/material/dialog';
import { ReplyButton } from '../reply-button/reply-button';
import {MatIconModule} from '@angular/material/icon';
import { CommentForm } from '../comment-form/comment-form';
@Component({
  selector: 'comment-tree',
  imports: [DatePipe, UserAvatar, ReplyButton, CommentTree, MatIconModule, CommentForm],
  templateUrl: './comment-tree.html'
})
export class CommentTree {
  private dialog = inject(MatDialog);
  
  @Input({ required: true }) comment!: CommentMessage;

  //Save reply to meeting
  @Input({ required: true }) meetingId!: number; 

  //Notify comment view
  @Output() replyAdded = new EventEmitter<void>();

  isReplying = false;

  toggleReply(){
    this.isReplying = !this.isReplying;
  }

  onReplySuccess(){
    this.isReplying = false;
    this.replyAdded.emit();
  }


  openParticipantDetails(userId: number, username: string): void 
  {
    this.dialog.open(UserDialog, 
    {
      data: {id: userId, username: username},
      width: '320px'
    });
  }

}
