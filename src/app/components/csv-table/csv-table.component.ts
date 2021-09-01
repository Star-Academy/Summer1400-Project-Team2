import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { parse } from 'papaparse';

@Component({
  selector: 'app-csv-table',
  templateUrl: './csv-table.component.html',
  styleUrls: ['./csv-table.component.scss']
})
export class CsvTableComponent implements OnInit, AfterViewInit {
  columns: string[] = [];
  dataSource = new MatTableDataSource<unknown>([]);

  @Input()
  csv = '';

  @ViewChild(MatSort)
  private sort!: MatSort;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  ngOnInit(): void {
    const parsedCsv = parse(this.csv, { header: true });

    this.dataSource = new MatTableDataSource(parsedCsv.data);
    this.columns = parsedCsv.meta.fields ?? [];
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
}
