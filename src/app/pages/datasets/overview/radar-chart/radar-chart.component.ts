import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartType, RadialChartOptions } from 'chart.js';
import { Label } from 'ng2-charts';

@Component({
  selector: 'app-radar-chart',
  templateUrl: './radar-chart.component.html',
  styleUrls: ['./radar-chart.component.scss']
})
export class RadarChartComponent {
  public radarChartOptions: RadialChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    legend: {
      display: false
    },
    scale: {
      ticks: {
        maxTicksLimit: 4,
        //TODO
        display: false
      },
      gridLines: {
        //TODO
        // display: false,
        color: 'hsl(6, 100%, 61%, .5)'
      },

      pointLabels: {
        fontSize: 14
      }
    }
  };

  public radarChartLabels: Label[] = [
    'اعتبار',
    'محبوبیت',
    'کامل بودن',
    'قابل کشف',
    'میزان کارکرد'
  ];

  public radarChartData: ChartDataSets[] = [
    {
      data: [85, 68, 60, 38, 86],
      backgroundColor: 'hsl(236, 55%, 48%, .5)',
      borderColor: 'hsl(236, 55%, 48%)'
    }
  ];
  public radarChartType: ChartType = 'radar';
}
