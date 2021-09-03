import {Component, Input, OnInit} from '@angular/core';
import {ChartType, RadialChartOptions} from 'chart.js';
import {MultiDataSet, Label} from 'ng2-charts';

@Component({
    selector: 'app-doughnut-chart',
    templateUrl: './doughnut-chart.component.html',
    styleUrls: ['./doughnut-chart.component.scss'],
})
export class DoughnutChartComponent implements OnInit {
    constructor() {}

    @Input() chartData!: any;
    @Input() chartColor!: any;
    @Input() chartLabel!: any;

    data = [];

    public donutColors = [{}];

    doughnutChartLabels: Label[] = ['invalid', 'valid'];
    doughnutChartData: MultiDataSet = [this.data];
    doughnutChartType: ChartType = 'doughnut';

    ngOnInit(): void {
        this.doughnutChartData = [this.chartData];
        this.donutColors = [{backgroundColor: this.chartColor}];
        this.doughnutChartLabels = this.chartLabel;
    }
}
