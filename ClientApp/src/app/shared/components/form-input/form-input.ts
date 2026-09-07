import { Component, Input } from '@angular/core';

@Component({
  selector: 'form-input',
  imports: [],
  templateUrl: './form-input.html'
})
export class FormInput {
  @Input() type: string ='';
  @Input() placeholder: string ='';
}
