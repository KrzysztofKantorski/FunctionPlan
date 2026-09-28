import { ChangeDetectorRef, Component, inject, Input, PLATFORM_ID } from '@angular/core';
import { GalleryModule, GalleryItem, ImageItem} from 'ng-gallery';
import { Navbar } from '../../nav/navbar/navbar';
import { BackButton } from '../../back-button/back-button';
import { MediaService } from '../../../../core/services/media-service';
import { MeetingMediaUrlsResponse } from '../../../../core/models/media/meetingMediaUrlsResponse';
import { isPlatformBrowser } from '@angular/common';
import { catchError, forkJoin, map, of, switchMap, take } from 'rxjs';
import { UserAvatar } from '../../meeting/user-avatar/user-avatar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MeetingParticipant } from '../../../../core/models/meeting/meeting-participant';
import { UserDialog } from '../../meeting/user-dialog/user-dialog';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-image-gallery',
  imports: [GalleryModule, Navbar, BackButton, UserAvatar, MatProgressSpinnerModule],
  templateUrl: './image-gallery.html'
})
export class ImageGallery {

  //Get meeting id from route
  @Input() id!: string;

  private mediaService = inject(MediaService)
  private cdr = inject(ChangeDetectorRef)
  private platformId = inject(PLATFORM_ID) 
  private dialog = inject(MatDialog);
  
  images: GalleryItem[] = [];
  mediaInfo: MeetingMediaUrlsResponse[] = [];

  //For ngOnDestroy
  private rawObjectUrls: string[] = [];

  currentIndex = 0;
  isLoading = true;


  ngOnInit() {

  //Check browser
  if(!isPlatformBrowser(this.platformId))
  {
    return;
  }


  const meetingId = Number(this.id);


  //Get media metadata
  this.mediaService.getMeetingMedia(meetingId).pipe(
    take(1),
    switchMap((data)=>{
      this.mediaInfo = data ?? [];
      if (this.mediaInfo.length === 0) 
      {
        return of([]);
      }
      console.log(this.mediaInfo);

      //Get media images
      const blobRequests = this.mediaInfo.map((item) =>
          this.mediaService.getMeetingImage(meetingId, item.fileName).pipe(
            map((blob: Blob) => {
              const url = URL.createObjectURL(blob);
              this.rawObjectUrls.push(url);
              return new ImageItem({ src: url, thumb: url });
            }),
            catchError((err) => {
              console.error(`Could not get image: ${item.fileName}`, err);

              //Empty image
              return of(new ImageItem({ src: '', thumb: '' }));
            })
          )
        );

      //Wait for all requests to finish
      return forkJoin(blobRequests);
    })

  ).subscribe
    ({ 
      next: (galleryItems)=>
      {
        //Assign blobs to gallery
        this.images = galleryItems;
        this.isLoading = false;
        this.cdr.markForCheck();
      },
      error: (err)=>
      {
        console.error('Could not get images:', err);
        this.isLoading = false;
        this.cdr.markForCheck();
      }
  })
  }


  onIndexChange(event: any): void {
    this.currentIndex = event?.currIndex ?? 0;
    this.cdr.markForCheck();
  }



  ngOnDestroy(): void {
    //Clear blobs
    for (const url of this.rawObjectUrls) {
      URL.revokeObjectURL(url);
    }
    this.rawObjectUrls = [];
  }


  openParticipantDetails(userId: number, userName: string): void 
    {
      const details: MeetingParticipant = 
      {
        id: userId, 
        username: userName, 
        profilePictureUrl: null
      }

      this.dialog.open(UserDialog, 
      {
        data: details,
        width: '320px'
      });
  }
}
