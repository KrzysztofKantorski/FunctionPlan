import { Component } from '@angular/core';
import { ServerErrorAnimation } from '../../../shared/components/server-error-animation/server-error-animation';
@Component({
  selector: 'page-not-found',
  imports: [ServerErrorAnimation],
  templateUrl: './page-not-found.html'
})
export class PageNotFound {}
