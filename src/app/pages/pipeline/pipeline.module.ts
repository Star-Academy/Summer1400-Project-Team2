import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';
import { PipelineHeaderComponent } from './pipeline-header/pipeline-header.component';
import {MatTooltipModule} from '@angular/material/tooltip';
@NgModule({
  declarations: [PipelinePage, PipelineHeaderComponent],
  imports: [CommonModule, PipelineRoutingModule,MatTooltipModule],
  exports:[PipelinePage]
})
export class PipelineModule {}
