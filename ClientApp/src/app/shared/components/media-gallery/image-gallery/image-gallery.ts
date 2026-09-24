import { Component } from '@angular/core';
import { GalleryModule, GalleryItem, ImageItem} from 'ng-gallery';
import { Navbar } from '../../nav/navbar/navbar';
import { BackButton } from '../../back-button/back-button';
@Component({
  selector: 'app-image-gallery',
  imports: [GalleryModule, Navbar, BackButton],
  templateUrl: './image-gallery.html'
})
export class ImageGallery {
  images: GalleryItem[] = [];

  ngOnInit() {
    // Set items array
  this.images = [
      new ImageItem({
        src: 'https://picsum.photos/id/1018/1000/600',
        thumb: 'https://picsum.photos/id/1018/200/120'
      }),
      new ImageItem({
        src: 'https://picsum.photos/id/1015/1000/600',
        thumb: 'https://picsum.photos/id/1015/200/120'
      })
    ];
  }
}
