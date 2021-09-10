import { Component } from '@angular/core';
export interface DatasetData {
  id: number;
  name: string;
  connectionName: string;
  created: string;
  star: boolean;
}

const DATA: DatasetData[] = [
  { id: 1, name: 'داده فیک', connectionName: 'دمو', created: 'امروز', star: true },
  { id: 2, name: 'داده فیک2', connectionName: 'دمو', created: 'ماه پیش', star: false }
];

@Component({
  selector: 'app-datasets',
  templateUrl: './dataset.component.html',
  styleUrls: ['./dataset.component.scss']
})
export class DatasetComponent {
  rowClick(row: unknown): void {
    console.log(row);
  }

  displayedColumns: string[] = ['name', 'connectionName', 'created', 'star'];
  dataSource = DATA;
}
