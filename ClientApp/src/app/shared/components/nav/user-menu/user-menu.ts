import { ChangeDetectorRef, Component, inject, Input, OnDestroy, OnInit, PLATFORM_ID } from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { UserService } from '../../../../core/services/user-service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { isPlatformBrowser } from '@angular/common';
@Component({
  selector: 'user-menu',
  imports: [MatMenuModule, MatButtonModule],
  templateUrl: './user-menu.html'
})
export class UserMenu implements OnInit, OnDestroy{
  private cdr = inject(ChangeDetectorRef);
  private userService = inject(UserService);
  private sanitizer = inject(DomSanitizer);
  private platformId = inject(PLATFORM_ID);

  avatarUrl: SafeUrl | null = null;
  private rawObjectUrl: string | null = null;
  
  
  ngOnInit()
  {
    if (isPlatformBrowser(this.platformId)) 
    {
        this.userService.getUserImage().subscribe({
          next: (blob: Blob) =>
          {
            this.rawObjectUrl = URL.createObjectURL(blob);
            const safe = this.sanitizer.bypassSecurityTrustUrl(this.rawObjectUrl);

            Promise.resolve().then(() => {
              this.avatarUrl = safe;
              this.cdr.markForCheck();
            });
          },
          error: () =>
          {
            Promise.resolve().then(() => {
              this.avatarUrl = null;
              this.cdr.markForCheck();
            });
          }
        })
      
    }
  }


  ngOnDestroy() 
  {
    if (this.rawObjectUrl) 
    {
      URL.revokeObjectURL(this.rawObjectUrl);
    }
  }
  

}
