import { Input } from '@angular/core';
import { AfterViewInit } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
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
  data = new MatTableDataSource<unknown>([]);

  @Input()
  csv = '';

  @ViewChild(MatSort)
  private sort!: MatSort;

  ngOnInit(): void {
    const parsedCsv = parse(this.csv, { header: true });

    this.data = new MatTableDataSource(parsedCsv.data);
    this.columns = parsedCsv.meta.fields ?? [];
  }

  ngAfterViewInit(): void {
    this.data.sort = this.sort;
  }
}
