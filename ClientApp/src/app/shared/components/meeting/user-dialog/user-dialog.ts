import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MeetingParticipant } from '../../../../core/models/meeting/meeting-participant';


@Component({
  selector: 'app-user-dialog',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './user-dialog.html'
})
export class UserDialog 
{
  data: MeetingParticipant = inject(MAT_DIALOG_DATA);
}
