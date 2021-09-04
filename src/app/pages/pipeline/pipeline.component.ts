import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
import { OgmaService } from './services/ogma.service';
@Component({
  selector: 'page-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelinePage {
  constructor(public dialog: MatDialog, private ogmaService: OgmaService) {}
  openDialog() {
    const dialogRef = this.dialog.open(ProcessorModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  onCreateFirstNode(){

    this.ogmaService.initConfig({
      container: "graph-container"      
  });

  this.ogmaService.ogma.events.onClick((event:any) => {
    if (event.target === null) {
        console.log("clicked on background at coordinates", event.x, event.y);
    } else if (event.target.isNode) {
        const nodeId = event.target.getId();
        const shape = this.ogmaService.ogma.getNode(nodeId).getAttribute("shape");
        console.log("clicked on a node with id",nodeId);
        if (shape === "circle") {
          this.ogmaService.onPluseNode(nodeId);
        }
    } else {
        let edge = event.target;
        this.ogmaService.clickOnEdge(edge);
    }
});

    this.ogmaService.createFirstNode();
  }

  onZoomInBtn(){
    this.ogmaService.setZoomIn();
  }
  onZoomOutBtn(){
    this.ogmaService.setZoomOut();
  }
}
