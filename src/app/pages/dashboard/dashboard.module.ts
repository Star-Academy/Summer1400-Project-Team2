import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DashboardComponent} from './dashboard.component';
import {PipelinesComponent} from './pipelines/pipelines.component';
import {DatasetsComponent} from './datasets/datasets.component';
import {RouterModule} from "@angular/router";
import {NavbarComponent} from './navbar/navbar.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatListModule} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import { ToolbarComponent } from './toolbar/toolbar.component';


@NgModule({
  declarations: [
    DashboardComponent,
    PipelinesComponent,
    DatasetsComponent,
    NavbarComponent,
    ToolbarComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
  ],
  exports: [
    DashboardComponent,
  ]
})
export class DashboardModule {
}
