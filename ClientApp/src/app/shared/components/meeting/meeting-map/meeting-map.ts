import { AfterViewInit, Component, ElementRef, EventEmitter, inject, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID } from '@angular/core';
import { environment } from '../../../../../environments/environment.development';


//Meeting coordinates - langitude, longitude
export interface MapCoordinates 
{
  lng: number;
  lat: number;
}


@Component({
  selector: 'meeting-map',
  standalone: true,
  templateUrl: './meeting-map.html'
})



export class MeetingMap implements AfterViewInit, OnDestroy {

  @ViewChild('mapContainer', { static: true }) mapContainer!: ElementRef;


  //Define two modes 
  //readOnly true for browsing meetings
  //false if adding location in form

  @Input() readOnly = true; 
  @Input() initialCoordinates?: MapCoordinates;


  //Emit location change
  @Output() locationSelected = new EventEmitter<MapCoordinates>();
  
  private platformId = inject(PLATFORM_ID);

  private map!: mapboxgl.Map;
  private marker?: mapboxgl.Marker;

  private resizeObserver!: ResizeObserver;
  

  async ngAfterViewInit()  
  {
    //Ensure browser
    if(isPlatformBrowser(this.platformId))
    {

      const mapboxgl = (await import('mapbox-gl')).default;

      //Force import 
      (mapboxgl as any).workerClass = class extends Worker {
        constructor() {
          super('/mapbox-gl-csp-worker.js');
        }
      };

      mapboxgl.accessToken = environment.mapboxToken;


      //Add default location
      const defaultCenter: [number, number] = this.initialCoordinates 
      ? [this.initialCoordinates.lng, this.initialCoordinates.lat] 
      : [18.6714, 50.2976];

      //New map instance
      this.map = new mapboxgl.Map({
        container: this.mapContainer.nativeElement, 
        style: 'mapbox://styles/mapbox/streets-v12',

        //Set meeting coordinates
        center: defaultCenter, 
        zoom: 14,
      });

      this.map.on("load", ()=>{
        //Set pin if read mode
        if (this.initialCoordinates) 
        {
          this.placeMarker(mapboxgl, this.initialCoordinates.lng, this.initialCoordinates.lat);
        }

        //When creating meeting
        if(!this.readOnly){
          this.map.on('click', (e: any)=>{

            //Get coordinates from passed event
            const { lng, lat } = e.lngLat;

            this.placeMarker(mapboxgl, lng, lat);

            //Emit changes
            this.locationSelected.emit({ lng, lat });
          })
        }


      });

      this.resizeObserver = new ResizeObserver(() => {
        if (this.map) {
          this.map.resize();
        }
      });

      this.resizeObserver.observe(this.mapContainer.nativeElement);
    }
  }


  private placeMarker(mapboxgl: any, lng: number, lat: number): void {
    if (this.marker) {
      this.marker.setLngLat([lng, lat]);
    } else {
      const newMarker = new mapboxgl.Marker({ 
        color: '#3b82f6',
        draggable: !this.readOnly 
      })
        .setLngLat([lng, lat])
        .addTo(this.map);

      this.marker = newMarker;

      //Emit changes after dragging pin
      if (!this.readOnly) {
        newMarker.on('dragend', () => {

          const lngLat = newMarker.getLngLat();
          //Emit location change
          this.locationSelected.emit({ lng: lngLat.lng, lat: lngLat.lat });
        });
      }
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
