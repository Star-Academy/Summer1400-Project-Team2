import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { PipelinesComponent } from './pages/dashboard/pipelines/pipelines.component';
import { LandingComponent } from './pages/landing/landing.component';
import { DatasetsComponent } from './pages/datasets/datasets.component';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { RegisterComponent } from './pages/register/register.component';
import { DatasetComponent } from './pages/dashboard/datasets/dataset.component';
import { PipelinePage } from './pages/pipeline/pipeline.component';

const routes: Routes = [
  { path: '', component: LandingComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: 'pipeline', component: PipelinesComponent },
      { path: 'dataset', component: DatasetComponent }
    ]
  },
  {
    path: 'pipelines',
    component: PipelinePage
  },
  { path: 'datasets', component: DatasetsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
