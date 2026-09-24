import { Component, inject, Input } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';


@Component({
  selector: 'meeting-info-dialog',
  imports: [
    MatButtonModule, 
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle,
  ],
  templateUrl: './meeting-info-dialog.html'
})


export class MeetingInfoDialog {
  readonly data = inject(MAT_DIALOG_DATA);

}
