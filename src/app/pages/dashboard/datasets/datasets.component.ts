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
  templateUrl: './datasets.component.html',
  styleUrls: ['./datasets.component.scss']
})
export class DatasetsComponent {
  rowClick(row: unknown): void {
    console.log(row);
  }

  displayedColumns: string[] = ['name', 'connectionName', 'created', 'star'];
  dataSource = DATA;
}
