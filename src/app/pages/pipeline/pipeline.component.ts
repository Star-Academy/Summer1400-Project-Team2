import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
@Component({
  selector: 'page-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelinePage {
  constructor(public dialog:MatDialog){}
  openDialog(){
    const dialogRef = this.dialog.open(ProcessorModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
