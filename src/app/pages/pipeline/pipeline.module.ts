import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';
import { PipelineHeaderComponent } from './pipeline-header/pipeline-header.component';
import {MatTooltipModule} from '@angular/material/tooltip';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [PipelinePage, PipelineHeaderComponent],
  imports: [CommonModule, PipelineRoutingModule,MatTooltipModule,FormsModule],
  exports:[PipelinePage]
})
export class PipelineModule {}
