import { Component } from '@angular/core';
import { MainHeader } from '../../../shared/components/main-header/main-header';
@Component({
  selector: 'app-main-page',
  imports: [MainHeader],
  templateUrl: './main-page.html'
})
export class MainPage {
  dummyMeetings = [1,2,3,4,5];
}
