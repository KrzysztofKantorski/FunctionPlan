import { Component, Input } from '@angular/core';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';


@Component({
  selector: 'form-input',
  imports: [MatInputModule, MatFormFieldModule],
  templateUrl: './form-input.html'
})
export class FormInput {
  @Input() type: string ='';
  @Input() placeholder: string ='';
}
  