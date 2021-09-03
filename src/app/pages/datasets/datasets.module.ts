import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DatasetsComponent} from './datasets.component';
import {SideMenuComponent} from './side-menu/side-menu.component';
import {TopMenuComponent} from './top-menu/top-menu.component';
import {OverviewComponent} from './overview/overview.component';
import {SampleComponent} from './sample/sample.component';
import {RadarChartComponent} from './overview/radar-chart/radar-chart.component';
import {ChartsModule} from 'ng2-charts';
import {DoughnutChartComponent} from './overview/doughnut-chart/doughnut-chart.component';
import {MatSliderModule} from '@angular/material/slider';
import {FormsModule} from '@angular/forms';

@NgModule({
    declarations: [
        DatasetsComponent,
        SideMenuComponent,
        TopMenuComponent,
        OverviewComponent,
        SampleComponent,
        RadarChartComponent,
        DoughnutChartComponent,
    ],
    imports: [CommonModule, ChartsModule, MatSliderModule, FormsModule],
})
export class DatasetsModule {}
