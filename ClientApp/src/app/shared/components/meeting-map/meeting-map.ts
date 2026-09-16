import { AfterViewInit, Component, ElementRef, inject, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID } from '@angular/core';
import { environment } from '../../../../environments/environment.development';

@Component({
  selector: 'meeting-map',
  standalone: true,
  templateUrl: './meeting-map.html'
})


export class MeetingMap implements AfterViewInit, OnDestroy {

  @ViewChild('mapContainer', { static: true }) mapContainer!: ElementRef;

  //Meeting coordinates
  @Input({ required: true }) lng!: number;
  @Input({ required: true }) lat!: number;

  private platformId = inject(PLATFORM_ID);
  private map: any;
  private resizeObserver!: ResizeObserver;
  

  async ngAfterViewInit()  
  {
    //Ensure browser
    if(isPlatformBrowser(this.platformId))
    {

      //Import mapbox
      const mapboxgl = (await import('mapbox-gl')).default;

      mapboxgl.accessToken = environment.mapboxToken;

      //New map instance
      this.map = new mapboxgl.Map({
        container: this.mapContainer.nativeElement, 
        style: 'mapbox://styles/mapbox/streets-v12',

        //Set meeting coordinates
        center: [this.lng, this.lat], 
        zoom: 14,
      });

      new mapboxgl.Marker({ color: '#3b82f6' })
        .setLngLat([this.lng, this.lat])
        .addTo(this.map);

      this.resizeObserver = new ResizeObserver(() => {
        if (this.map) {
          this.map.resize();
        }
      });

      this.resizeObserver.observe(this.mapContainer.nativeElement);
    }
  }


  ngOnDestroy(): void 
  {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
    if (this.map) {
      this.map.remove();
    }
  }

}
