import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DatasetsComponent} from './datasets.component';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { OverviewComponent } from './overview/overview.component';
import { SampleComponent } from './sample/sample.component';

@NgModule({
    declarations: [DatasetsComponent, SideMenuComponent, TopMenuComponent, OverviewComponent, SampleComponent],
    imports: [CommonModule],
})
export class DatasetsModule {}
