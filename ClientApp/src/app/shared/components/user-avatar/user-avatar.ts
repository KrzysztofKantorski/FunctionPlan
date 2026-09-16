import { ChangeDetectorRef, Component, inject, Input, PLATFORM_ID } from '@angular/core';
import { UserService } from '../../../core/services/user-service';
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

  private userService = inject(UserService);
  private sanitizer = inject(DomSanitizer);
  private platformId = inject(PLATFORM_ID);
  private cdr = inject(ChangeDetectorRef);

  avatarUrl: SafeUrl | null = null;
  private rawObjectUrl: string | null = null;


  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {


      //Get blob object
      this.userService.getUserImage().subscribe({

        next: (blob: Blob) => 
        {
          this.rawObjectUrl = URL.createObjectURL(blob);
          const safeUrl = this.sanitizer.bypassSecurityTrustUrl(this.rawObjectUrl);

          setTimeout(() => {
            this.avatarUrl = safeUrl;
            this.cdr.markForCheck();
          });
          
        },

        error: () => 
        {
          this.avatarUrl = null;
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.rawObjectUrl) {
      URL.revokeObjectURL(this.rawObjectUrl);
    }
  }
}
