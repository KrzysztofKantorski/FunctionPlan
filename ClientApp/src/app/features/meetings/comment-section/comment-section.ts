import { Component, inject, Input } from '@angular/core';
import { CommentHeader } from '../../../shared/components/comment-header/comment-header';
import { CommentService } from '../../../core/services/comment-service';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
@Component({
  selector: 'comment-section',
  imports: [CommentHeader],
  templateUrl: './comment-section.html'
})
export class CommentSection {
  private commentService = inject(CommentService);

  //Get meeting id from route
  @Input({ required: true }) meetingId!: number;

  private refreshSubject = new BehaviorSubject<void>(undefined);

  comments$!: Observable<Comment[]>;

  ngOnInit(): void{
    this.comments$ = this.refreshSubject.pipe(
      switchMap(() => this.commentService.getComments(this.meetingId))
    );
  }
}
