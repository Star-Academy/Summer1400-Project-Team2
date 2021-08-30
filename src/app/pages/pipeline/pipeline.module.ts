import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';

@NgModule({
  declarations: [PipelinePage],
  imports: [CommonModule, PipelineRoutingModule]
})
export class PipelineModule {}
