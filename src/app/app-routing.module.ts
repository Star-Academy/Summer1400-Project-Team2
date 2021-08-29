import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DatasetsComponent} from './pages/datasets/datasets.component';

const routes: Routes = [{path: 'datasets', component: DatasetsComponent}];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
