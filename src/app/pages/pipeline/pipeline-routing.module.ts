import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PipelinePage } from './pipeline.component';

const routes: Routes = [
  {
    path: ':pipelineId',
    component: PipelinePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PipelineRoutingModule {}
