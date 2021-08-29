import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { PipelinesComponent } from './pipelines/pipelines.component';
import { DatasetsComponent } from './datasets/datasets.component';



@NgModule({
  declarations: [
    DashboardComponent,
    PipelinesComponent,
    DatasetsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class DashboardModule { }
