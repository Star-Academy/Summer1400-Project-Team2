import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./pages/dashboard/dashboard.component";
import {PipelinesComponent} from "./pages/dashboard/pipelines/pipelines.component";
import {DatasetsComponent} from "./pages/dashboard/datasets/datasets.component";

const routes: Routes = [  {path:'dashboard', component:DashboardComponent, children:[
    {path:'pipelines', component:PipelinesComponent},
    {path:'datasets', component:DatasetsComponent},
  ]},
  {
    path: 'pipeline',
    loadChildren: () => import('./pages/pipeline/pipeline.module').then(m => m.PipelineModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
