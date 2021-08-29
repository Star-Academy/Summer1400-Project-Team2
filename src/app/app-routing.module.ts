import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./dashboard/dashboard.component";
import {PipelinesComponent} from "./dashboard/pipelines/pipelines.component";
import {DatasetsComponent} from "./dashboard/datasets/datasets.component";

const routes: Routes = [
  {path:'dashboard', component:DashboardComponent, children:[
      {path:'pipelines', component:PipelinesComponent},
      {path:'datasets', component:DatasetsComponent},
    ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
