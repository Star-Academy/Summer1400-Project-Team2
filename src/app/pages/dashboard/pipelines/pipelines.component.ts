import {Component, OnInit} from '@angular/core';
import {DashboardService} from "../../../services/dashboard/dashboard.service";
import {tableData} from "../tableData";
import {Router} from "@angular/router";


@Component({
  selector: 'app-pipelines',
  templateUrl: './pipelines.component.html',
  styleUrls: ['./pipelines.component.scss']
})
export class PipelinesComponent implements OnInit {

  constructor(private dashboardService: DashboardService, private router:Router) {
  }

  table: tableData[] = [{name:'1',id:'2'}];

  ngOnInit() {
    this.dashboardService.tableData1('pipeline').subscribe(data => {
      this.table = data;
    })
  }

  rowClick(row: tableData): void {
    // console.log(row);
    this.router.navigateByUrl('/pipelines/'+ row.id);
  }

  create(id: string, name: string, entryDB: string, finalDB: string): void {
    this.dashboardService.create(id, name, entryDB, finalDB);
  }

  displayedColumns: string[] = ['id', 'name'];
   dataSource = this.table;
}
