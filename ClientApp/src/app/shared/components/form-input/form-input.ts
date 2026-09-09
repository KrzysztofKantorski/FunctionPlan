import { Component, forwardRef, Input } from '@angular/core';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';


@Component({
  selector: 'form-input',
  imports: [MatInputModule, MatFormFieldModule, FormsModule],
  templateUrl: './form-input.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormInput),
      multi: true
    }
  ]
})


export class FormInput {
  @Input() type: string ='';
  @Input() placeholder: string ='';

  // Wewnętrzna wartość inputa
  value: string = '';
  isDisabled: boolean = false;

  // Puste funkcje, które Angular nadpisze swoimi metodami
  onChange: any = () => {};
  onTouch: any = () => {};

  // 1. Angular wysyła wartość z FormBuilder do Twojego komponentu
  writeValue(value: any): void {
    this.value = value || '';
  }

  // 2. Rejestruje funkcję wywoływaną, gdy użytkownik coś wpisze
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  // 3. Rejestruje funkcję wywoływaną, gdy użytkownik opuści pole (blur)
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  // 4. Obsługa blokowania pola (np. form.disable())
  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  // Metoda wywoływana, gdy wewnętrzna wartość się zmieni
  onModelChange(value: string) {
    this.value = value;
    this.onChange(value); // Powiadamia FormBuilder o nowej wartości
  }
}
  