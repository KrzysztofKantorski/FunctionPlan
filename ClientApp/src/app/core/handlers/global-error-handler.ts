import { ErrorHandler, Injectable, NgZone, inject } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    private snackBar = inject(MatSnackBar);
    private zone = inject(NgZone);

    handleError(error: any): void 
    {
        this.zone.run(()=>{
            console.error('Global error occured:', error);
            this.snackBar.open('An error occurred.', 'Close', { panelClass: 'error-snackbar', duration:5000 });
        })
    }
}