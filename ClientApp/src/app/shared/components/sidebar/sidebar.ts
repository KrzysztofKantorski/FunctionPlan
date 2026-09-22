import { Component, EventEmitter, inject, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MeetingFilters } from '../../../core/models/meeting/meeting-filters';
import { debounce, debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'sidebar',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './sidebar.html'
})
export class Sidebar {

  //form builder 
  private fb = inject(FormBuilder);

  //Emit filters to meeting component
  @Output() filtersChanged = new EventEmitter<MeetingFilters>();

  filterForm = this.fb.group
  ({
    startDate: [null as Date | null],
    endDate: [null as Date | null],
    sortOrder: ['asc'],
    status: [null as number | null]
  });

  ngOnInit()
  {
    this.filterForm.valueChanges.pipe(

      //Add small delay
      debounceTime(300),

      //Check if changes were made
      distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr))

    ).subscribe(value =>{
      this.filtersChanged.emit(value as MeetingFilters)
    })
  }
}
