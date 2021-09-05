import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DatasetsComponent } from './pages/datasets/datasets.component';
import { LandingComponent } from './pages/landing/landing.component';

const routes: Routes = [
  { path: '', component: LandingComponent },
  {
    path: 'pipeline',
    loadChildren: () =>
      import('./pages/pipeline/pipeline.module').then(m => m.PipelineModule)
  },
  { path: 'datasets', component: DatasetsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
