import { ChangeDetectorRef, Component, inject, Input, PLATFORM_ID, SimpleChanges } from '@angular/core';
import { UserService } from '../../../../core/services/user-service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'user-avatar',
  imports: [],
  templateUrl: './user-avatar.html'
})

export class UserAvatar {
  @Input({ required: true }) userId!: number;
  @Input({ required: true }) username!: string;

  @Input({required: true}) profilePictureUrl: string | null = null;

  private userService = inject(UserService);
  private sanitizer = inject(DomSanitizer);
  private platformId = inject(PLATFORM_ID);
  private cdr = inject(ChangeDetectorRef);

  avatarUrl: SafeUrl | null = null;
  private rawObjectUrl: string | null = null;

  ngOnChanges(changes: SimpleChanges): void{

    if (!isPlatformBrowser(this.platformId))
    {
      return;
    } 

    //User does not have image
    if (!this.profilePictureUrl) 
    {
      this.cleanupUrl();
      this.avatarUrl = null;
      return;
    }

    //User has an image, changes were made
    if (changes['userId'] || changes['profilePictureUrl']) 
    {
      this.loadAvatarBlob();
    }


  }


  

  private cleanupUrl(): void {
    if (this.rawObjectUrl) {
      URL.revokeObjectURL(this.rawObjectUrl);
      this.rawObjectUrl = null;
    }
  }


  private loadAvatarBlob(): void {
    this.cleanupUrl();

    //Get image
    this.userService.getAnotherUserImage(this.userId)
    .subscribe({
      next: (blob: Blob) => {
        this.rawObjectUrl = URL.createObjectURL(blob);
        this.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(this.rawObjectUrl);
        this.cdr.markForCheck();
        console.log(this.avatarUrl);
      },
      error: () => {

        // Clear user image
        this.cleanupUrl();
        this.avatarUrl = null;
        this.cdr.markForCheck();
      }
    });
  }

  ngOnDestroy(): void 
  {
    if (this.rawObjectUrl) {
      URL.revokeObjectURL(this.rawObjectUrl);
    }
  }


  onAvatarError(event: Event): void 
  {
    //Server returned 404
    const imgElement = event.target as HTMLImageElement;
    //Stop infinite loop
    imgElement.onerror = null; 
  }

}
