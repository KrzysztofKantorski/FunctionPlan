import { Component, inject } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { BackButton } from '../../../shared/components/back-button/back-button';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormInput } from '../../../shared/components/form-input/form-input';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms'
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MeetingMap } from '../../../shared/components/meeting/meeting-map/meeting-map';
import { MapCoordinates } from '../../../shared/components/meeting/meeting-map/meeting-map';
@Component({
  selector: 'meeting-create',
  imports: [Navbar, BackButton, NavbarBtnGroup, UserMenu, FormInput, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatSelectModule, 
    MatDatepickerModule, MatButtonModule, MeetingMap],
  templateUrl: './meeting-create.html',
  providers: [provideNativeDateAdapter()],
})
export class MeetingCreate {

  private fb = inject(FormBuilder);

  // Zadeklarowane WSZYSTKIE pola używane w formControlName w HTML
  meetingForm = this.fb.group({
    title: ['', [Validators.required]],
    description: [''],
    date: [null as Date | null],
    latitude: [null as number | null, Validators.required],
    longitude: [null as number | null, Validators.required]
  });

  onLocationSelected(coords: MapCoordinates): void {
    // Wpisujemy pobrane X (lng) i Y (lat) z mapy prosto do formularza
    this.meetingForm.patchValue({
      longitude: coords.lng,
      latitude: coords.lat
    });

    console.log('Wybrane koordynaty:', coords);
  }


  onSubmit(){
    if (this.meetingForm.valid) {
      const payload = this.meetingForm.value;
    }
  }
}

