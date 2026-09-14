import { Component, EventEmitter, Output } from '@angular/core';
import { MatFormField } from '@angular/material/input';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';
@Component({
  selector: 'navbar-search',
  imports: [MatFormField, ReactiveFormsModule, MatInputModule, MatIconModule, MatButtonModule],
  templateUrl: './navbar-search.html'
})
export class NavbarSearch {
  searchControl = new FormControl("");

  //Emit search text
  @Output() searchChanged = new EventEmitter<string>();

  ngOnInit(){
    this.searchControl.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(
      value =>{
        this.searchChanged.emit(value || "");
      }
    )
  }


  clearSearch()
  {
    this.searchControl.setValue("");
  }
}
