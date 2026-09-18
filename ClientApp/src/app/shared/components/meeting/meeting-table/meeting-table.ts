import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { MeetingTableView } from '../../../../core/models/meeting-table';
import { Observable } from 'rxjs';



@Component({
  selector: 'meeting-table',
  imports: [MatTableModule, MatIconModule, DatePipe],
  templateUrl: './meeting-table.html'
})


export class MeetingTable {
  private router = inject(Router);

  @Input() isOrganizerView = false; 

  @Output() updateMeeting = new EventEmitter<number>();
  @Output() deleteMeeting = new EventEmitter<number>();

  //Datesource
  dataSource = new MatTableDataSource<MeetingTableView>([]);

  displayedColumns: string[] = ['title', 'scheduledFor', 'organizerName', 'actions'];
  @Input({ required: true }) 
  set data(value: MeetingTableView[] | undefined) 
  {
    this.dataSource.data = value ?? [];
  }

  ngOnInit(): void {
    
    //If view is organized meetings add action buttons
    if (this.isOrganizerView) {
      this.displayedColumns = ['title', 'scheduledFor', 'organizerName', 'manage-actions', 'actions'];
    }

    
  }

  goToMeeting(meetingId: number): void {
    this.router.navigate(['/meeting-info', meetingId]);
  }

  onEdit(event: MouseEvent, id: number): void {
    event.stopPropagation(); 
    this.updateMeeting.emit(id);
  }

  onDelete(event: MouseEvent, id: number): void {
    event.stopPropagation();
    this.deleteMeeting.emit(id);
  }

}
