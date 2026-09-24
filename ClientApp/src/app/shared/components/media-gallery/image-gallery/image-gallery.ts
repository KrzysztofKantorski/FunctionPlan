import { Component, inject, Input } from '@angular/core';
import { GalleryModule, GalleryItem, ImageItem} from 'ng-gallery';
import { Navbar } from '../../nav/navbar/navbar';
import { BackButton } from '../../back-button/back-button';
import { MediaService } from '../../../../core/services/media-service';
import { MeetingMediaUrlsResponse } from '../../../../core/models/media/meetingMediaUrlsResponse';
@Component({
  selector: 'app-image-gallery',
  imports: [GalleryModule, Navbar, BackButton],
  templateUrl: './image-gallery.html'
})
export class ImageGallery {

  //Get meeting id from route
  @Input() id!: string;

  private mediaService = inject(MediaService)
  
  images: GalleryItem[] = [];
  mediaInfo: MeetingMediaUrlsResponse[] = [];

  isLoading = true;
  ngOnInit() {

  const meetingId = Number(this.id);

  this.mediaService.getMeetingMedia(meetingId).subscribe({
    next: (data)=>{
      this.mediaInfo = data;
      console.log(this.mediaInfo);
      this.isLoading = false;
    },
    error: (err)=>{
      console.error("cannot get media", err);
      this.isLoading = false;
    }

  })

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
