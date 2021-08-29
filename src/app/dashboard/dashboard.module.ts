import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DashboardComponent} from './dashboard.component';
import {PipelinesComponent} from './pipelines/pipelines.component';
import {DatasetsComponent} from './datasets/datasets.component';
import {RouterModule} from "@angular/router";


@NgModule({
  declarations: [
    DashboardComponent,
    PipelinesComponent,
    DatasetsComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    DashboardComponent,
  ]
})
export class DashboardModule {
}
