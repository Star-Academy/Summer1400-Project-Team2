import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProcessorModalComponent } from './components/modals/processor-modal/processor-modal.component';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public dialog:MatDialog){}
  openDialog(){
    const dialogRef = this.dialog.open(ProcessorModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
