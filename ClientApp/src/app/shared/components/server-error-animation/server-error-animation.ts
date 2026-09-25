import { Component } from '@angular/core';
import { LottieComponent, AnimationOptions } from 'ngx-lottie';
import { BackButton } from '../back-button/back-button';
@Component({
  selector: 'server-error-animation',
  imports: [LottieComponent, BackButton],
  templateUrl: './server-error-animation.html'
})
export class ServerErrorAnimation {
  readonly lottieOptions: AnimationOptions = {
    path: '/server-error.json', 
    loop: true,                 
    autoplay: true              
  };
}
