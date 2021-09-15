import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params } from '@angular/router';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
import { OgmaService } from './services/ogma.service';
@Component({
  selector: 'page-pipeline',
  templateUrl: './pipeline.component.html',
  styleUrls: ['./pipeline.component.scss']
})
export class PipelinePage implements OnInit {
  constructor(public dialog: MatDialog, private ogmaService: OgmaService,private route:ActivatedRoute) {}
  processor = '';
  deleteNode = false;
  pipelineId =0;  
  ngOnInit() {
    this.route.params.subscribe(
      (params:Params)=>{
        this.pipelineId = params['id'];
        this.ogmaService.setPipelineId(this.pipelineId);
      }
    )
    this.onCreateFirstNode();
    this.ogmaService.loadPipeline(this.pipelineId);
    
  }
  async onCreateFirstNode() {
    this.ogmaService.initConfig({
      container: 'graph-container',
      options: {
        backgroundColor: 'rgb(212, 212, 212)'
      }
    });

    this.ogmaService.ogma.events.onKeyPress('del', this.ogmaService.deleteNode);

    this.ogmaService.ogma.events.onClick((event: any) => {
      if (event.target === null) {
        console.log('clicked on background at coordinates', event.x, event.y);
      } else if (event.target.isNode) {
        const nodeId = event.target.getId();
        const shape = this.ogmaService.ogma.getNode(nodeId).getAttribute('shape');        
        if (nodeId === 0) {
          alert('دیتاست مبدا را انتخاب کنید.');
        }
        if (nodeId === 2) {
          alert('دیتاست مقصد را انتخاب کنید.');
        } else {
          if (shape === 'circle') {
            const dialogRef = this.dialog.open(ProcessorModalComponent);
            dialogRef.afterClosed().subscribe(result => {
              let flag = 0;
              if (result.event == 'filter') {
                this.processor = 'filter';
                flag = 1;
              } else if (result.event == 'aggregate') {
                this.processor = 'aggregate';
                flag = 1;
              } else if (result.event == 'join') {
                this.processor = 'join';
                flag = 1;
              }
              if (flag) {
                this.ogmaService.onPluseNode(nodeId, this.processor);
              }
            });
          }
        }
      } else {
        const edge = event.target;
        this.ogmaService.clickOnEdge(edge);
      }
    });
    this.ogmaService.loadPipeline(this.pipelineId).subscribe(res=>{
      console.log(res);
      if(Object.keys(res).length >0){
        console.log('full');        
        this.ogmaService.loadGraph();
      }else{
        console.log('empty');        
        this.ogmaService.createFirstNode();
         this.ogmaService.exportGraph().then(ogmaJson=>{          
          this.ogmaService.sendPipeline(this.pipelineId,ogmaJson);
        });
      }
    });

  }

  onZoomInBtn() {
    this.ogmaService.setZoomIn();
  }
  onZoomOutBtn() {
    this.ogmaService.setZoomOut();
  }
  async onExportBtn(){
    const res = await this.ogmaService.exportGraph();
   console.log(res);
  }

  onClearGraph(){
    this.ogmaService.sendPipeline(this.pipelineId,{});
  }
}
