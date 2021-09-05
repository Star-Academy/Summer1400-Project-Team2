import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DatasetsComponent } from './pages/datasets/datasets.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: 'pipeline',
    loadChildren: () =>
      import('./pages/pipeline/pipeline.module').then(m => m.PipelineModule)
  },
  { path: 'datasets', component: DatasetsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
