import { Component } from '@angular/core';
import { GalleryModule, GalleryItem, ImageItem, VideoItem, YoutubeItem, IframeItem } from 'ng-gallery';

@Component({
  selector: 'app-image-gallery',
  imports: [GalleryModule],
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
