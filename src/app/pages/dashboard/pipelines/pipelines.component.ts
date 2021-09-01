import { Component } from '@angular/core';

export interface PipelineData {
  id: number;
  name: string;
  connectionName: string;
  created: string;
  star: boolean;
}

const DATA: PipelineData[] = [
  { id: 1, name: 'داده فیک', connectionName: 'دمو', created: 'امروز', star: true },
  { id: 2, name: 'داده فیک2', connectionName: 'دمو', created: 'ماه پیش', star: false }
];

@Component({
  selector: 'app-pipelines',
  templateUrl: './pipelines.component.html',
  styleUrls: ['./pipelines.component.scss']
})
export class PipelinesComponent {
  rowClick(row: unknown): void {
    console.log(row);
  }

  displayedColumns: string[] = ['name', 'connectionName', 'created', 'star'];
  dataSource = DATA;
}
