import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
@Component({
  selector: 'meeting-dialog',
  imports: [MatButtonModule, MatDialogModule],
  templateUrl: './meeting-dialog.html'
})
export class MeetingDialog 
{
  data: string = inject(MAT_DIALOG_DATA);
}
