import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipelinePage } from './pipeline.component';
import { PipelineRoutingModule } from './pipeline-routing.module';
import { PipelineHeaderComponent } from './pipeline-header/pipeline-header.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormsModule } from '@angular/forms';
import { ProcessorModalComponent } from './processor-modal/processor-modal.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CsvTableModule } from '../../components/csv-table/csv-table.module';
import { PreviewComponent } from './preview/preview.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
  declarations: [
    PipelinePage,
    PipelineHeaderComponent,
    ProcessorModalComponent,
    PreviewComponent,
    SidebarComponent
  ],
  imports: [
    CommonModule,
    PipelineRoutingModule,
    MatTooltipModule,
    FormsModule,
    MatDialogModule,
    CsvTableModule
  ],
  providers: [MatDialog],
  exports: [PipelinePage]
})
export class PipelineModule {}
