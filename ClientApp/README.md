# FUNCTION PLAN angular client app


## Architecture & Core Concepts

The frontend application follows Angular best practices, promoting separation of concerns, reactivity, and reusable component patterns.


## Key Frontend Features


1. HTTP Interceptors (Pipeline Control)

* **Auth Interceptor:** Automatically attaches Authorization: Bearer <token> to outgoing HTTP requests. Handles HTTP 401 errors by triggering JWT refresh requests.
* **Error Interceptor:** Global error handling interceptor catching HTTP error responses and displaying notifications via Angular Material MatSnackBar.


2. Reactive Data Streams (RxJS)

State handling using BehaviorSubject and Observable streams for authentication status, current user profile, and active event filters.


3. UI/UX & Styling (Angular Material)
* **UI design** built with Angular Material components (MatCard, MatDialog, MatTable, MatFormField).


4. Interactive Maps
* **Integration with Mapbox** for real-time geolocation filtering and event location picking.



## Tech Stack & Dependencies

* Framework: Angular (v17+)

* State & Reactive Programming: RxJS

* UI Component Library: Angular Material

* HTTP Client: Angular HttpClient with Interceptors

* Maps Integration: Mapbox GL JS / Ngx-Mapbox

* Routing: Angular Router with Lazy Loading modules/routes



# Installation & Run

1. Navigate to the frontend directory:
   
```
cd src/ClientApp
```


2. Install dependencies

```
npm install
```


3. Configure Environment:
   
Update src/environments/environment.ts with your local API address and Mapbox token:

```
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7001/api',
  mapboxToken: 'YOUR_MAPBOX_ACCESS_TOKEN'
};
```


4. Start frontend server

```
ng serve
```

