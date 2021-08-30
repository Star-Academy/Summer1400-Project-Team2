import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';
import { PipelineHeaderComponent } from './pipeline-header/pipeline-header.component';
@NgModule({
  declarations: [PipelinePage, PipelineHeaderComponent],
  imports: [CommonModule, PipelineRoutingModule],
  exports:[PipelinePage]
})
export class PipelineModule {}
