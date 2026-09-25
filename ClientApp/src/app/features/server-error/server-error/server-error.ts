import { Component, Input } from '@angular/core';

import { LottieComponent, AnimationOptions } from 'ngx-lottie';
@Component({
  selector: 'server-error',
  imports: [LottieComponent],
  templateUrl: './server-error.html'
})
export class ServerError {
  
  readonly lottieOptions: AnimationOptions = {
    path: '/server-error.json', 
    loop: true,                 
    autoplay: true              
  };
  ngOnInit()
  {
    //window.location.href= '/main-page';
  }
}
