import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
import { OgmaService } from './services/ogma.service';
@Component({
  selector: 'page-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})

export class PipelinePage implements OnInit {
  constructor(public dialog: MatDialog, private ogmaService: OgmaService) {}
  processor='';
  ngOnInit(){
    this.onCreateFirstNode();
  }
  openDialog() {
    const dialogRef = this.dialog.open(ProcessorModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  onCreateFirstNode(){

    this.ogmaService.initConfig({
      container: "graph-container" ,
      options: {
        backgroundColor: 'rgb(212, 212, 212)'
      }     
  });

  this.ogmaService.ogma.events.onClick((event:any) => {
    if (event.target === null) {
        console.log("clicked on background at coordinates", event.x, event.y);
    } else if (event.target.isNode) {
        const nodeId = event.target.getId();
        const shape = this.ogmaService.ogma.getNode(nodeId).getAttribute("shape");
        console.log("clicked on a node with id",nodeId);
        if(nodeId=="n0"){
          alert('دیتاست مبدا را انتخاب کنید.');
        }
        if(nodeId =="n2"){
          alert('دیتاست مقصد را انتخاب کنید.');
        }
        if (shape === "circle") {
          const dialogRef = this.dialog.open(ProcessorModalComponent);
          dialogRef.afterClosed().subscribe(result => {
            let flag=0;
            if(result.event =='filter'){
              this.processor = 'filter';
              flag=1;
            }else if(result.event =='aggregate'){
              this.processor = 'aggregate';
              flag=1;
            }else if(result.event =='join'){
              this.processor = 'join';
              flag=1;
            }
            if(flag){
              this.ogmaService.onPluseNode(nodeId,this.processor);
            }
          });
          
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
