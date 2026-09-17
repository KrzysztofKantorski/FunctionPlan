import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe } from '@angular/common';
export interface PeriodicElement {
  scheduledFor: string;
  title: number;
  organizerName: number;
  actions: string;
}


const ELEMENT_DATA: PeriodicElement[] = [
  {title: 1, scheduledFor: '2025-10-10', organizerName: 1.0079, actions: 'H'}
];

@Component({
  selector: 'meeting-table',
  imports: [MatTableModule, MatIconModule, DatePipe],
  templateUrl: './meeting-table.html'
})


export class MeetingTable {
  displayedColumns: string[] = ['title', 'scheduledFor', 'organizerName', 'actions'];
  dataSource = ELEMENT_DATA;
}
