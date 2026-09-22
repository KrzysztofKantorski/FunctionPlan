import { Component, inject } from '@angular/core';
import { FormInput } from '../../form-input/form-input';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActionButton } from '../../action-button/action-button';
@Component({
  selector: 'comment-form',
  imports: [FormInput, ActionButton, ReactiveFormsModule],
  templateUrl: './comment-form.html'
})
export class CommentForm {
  private fb = inject(FormBuilder);

  //Form fields
  commentForm = this.fb.nonNullable.group({
    content: ['', 
      [Validators.required]
    ]
  });

  onSubmit(){
    console.log("siyt")
  }

}
