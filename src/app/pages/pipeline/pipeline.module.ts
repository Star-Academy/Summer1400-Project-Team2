import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';
import { PipelineHeaderComponent } from './pipeline-header/pipeline-header.component';
import {MatTooltipModule} from '@angular/material/tooltip';
import { FormsModule } from '@angular/forms';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
@NgModule({
  declarations: [PipelinePage, PipelineHeaderComponent, ProcessorModalComponent],
  imports: [CommonModule, PipelineRoutingModule,MatTooltipModule,FormsModule,MatDialogModule],
  providers:[MatDialog],
  exports:[PipelinePage]
})
export class PipelineModule {}
