import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormInput } from '../../form-input/form-input';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActionButton } from '../../action-button/action-button';
import { CommentRequest } from '../../../../core/models/comment/commentRequest';
import { CommentService } from '../../../../core/services/comment-service';
@Component({
  selector: 'comment-form',
  imports: [FormInput, ActionButton, ReactiveFormsModule],
  templateUrl: './comment-form.html'
})
export class CommentForm {
  private commentService = inject(CommentService);
  private fb = inject(FormBuilder);

  @Input({required: true}) meetingId!: number;
  @Input() parentCommentId: number | null = null;

  //Reload comments afted writing
  @Output() commentAdded= new EventEmitter<void>();

  isSubmitting = false;

  //Form fields
  commentForm = this.fb.nonNullable.group({
    commentContent: ['', 
      [Validators.required]
    ]
  });

  get commentContent()
  {
    return this.commentForm.controls.commentContent;
  }

  onSubmit(){
    if (this.commentForm.invalid || this.isSubmitting)
    {
      return;
    } 

    this.isSubmitting = true;

    //Create request object
    const request: CommentRequest = 
    {
      content: this.commentContent.value,
      parentCommentId: this.parentCommentId
    };

    this.commentService.sendComment(this.meetingId, request).subscribe({
      next: () => {
        this.commentForm.reset();
        this.isSubmitting = false;

        //Notify comment view
        this.commentAdded.emit();
      },
      error: (err) => {
        console.error('Cannot add comment:', err);
        this.isSubmitting = false;
      }
    })
  }

}
