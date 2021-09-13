import { Component, AfterViewInit } from '@angular/core';
import {DashboardService} from "../../../services/dashboard/dashboard.service";

interface Data {
  name: string
}

let res: any;

let DATA: Data[] = [
  { name: 'فیک'},
  { name: 'فیک2'},
];
@Component({
  selector: 'app-pipelines',
  templateUrl: './pipelines.component.html',
  styleUrls: ['./pipelines.component.scss']
})
export class PipelinesComponent implements AfterViewInit {
  constructor(private dashboardService: DashboardService) {
  }
  ngAfterViewInit() {
    res = this.dashboardService.tableData('pipelines');
    DATA = res;
  }

  rowClick(row: unknown): void {
    console.log(row);
  }

  displayedColumns: string[] = ['name'];
  dataSource = DATA;
}
