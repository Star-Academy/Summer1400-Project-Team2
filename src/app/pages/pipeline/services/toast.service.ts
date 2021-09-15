import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";
@Injectable({
  providedIn: 'root'
})
export class ToastService {
  constructor(private _snackBar: MatSnackBar) {}

  public openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      // horizontalPosition:'right',
      // verticalPosition: 'top',
      duration: 3000,
    });
  }
}
