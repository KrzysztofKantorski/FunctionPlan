import { Component, inject, Input, OnDestroy, OnInit, PLATFORM_ID } from '@angular/core';
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
            this.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(this.rawObjectUrl);
          },
          error: (err) =>
          {
            console.error('Avatar fetching error', err);
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
