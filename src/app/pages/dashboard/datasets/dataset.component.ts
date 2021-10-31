import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { tableData } from '../tableData';
import { Router } from '@angular/router';

@Component({
  selector: 'app-datasets',
  templateUrl: './dataset.component.html',
  styleUrls: ['./dataset.component.scss']
})
export class DatasetComponent implements OnInit {
  constructor(private dashboardService: DashboardService, private router: Router) {}

  table: tableData[] = [{ name: '1', id: '2' }];

  ngOnInit() {
    this.dashboardService.tableData1('dataset').subscribe(data => {
      this.table = data;
    });
  }

  rowClick(row: tableData): void {
    // console.log(row);
    this.router.navigateByUrl('/datasets');
  }

  create(id: string, name: string, entryDB: string, finalDB: string): void {
    this.dashboardService.create(id, name, entryDB, finalDB);
  }

  displayedColumns: string[] = ['id', 'name'];
  dataSource = this.table;
}
