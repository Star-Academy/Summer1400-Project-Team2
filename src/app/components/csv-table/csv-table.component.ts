import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CsvService } from 'src/app/services/csv/csv.service';

@Component({
  selector: 'app-csv-table',
  templateUrl: './csv-table.component.html',
  styleUrls: ['./csv-table.component.scss']
})
export class CsvTableComponent implements AfterViewInit {
  columns: string[] = [];
  dataSource = new MatTableDataSource<unknown>([]);

  @Input()
  bigData = false;

  // --------------------------
  // CSV Parsing

  @Input()
  csv = '';

  @Input()
  header = true;

  @Input()
  delimiter?: string;

  @Input()
  endOfLine?: string;

  @Input()
  escapeChar?: string;

  @Input()
  quoteChar?: string;

  @Input()
  comments?: string | boolean;

  // --------------------------
  // Paginator

  @Input()
  pageSizeOptions: number[] = [10, 20, 100, 200, 500, 1000];

  @Input()
  defaultPageSize = 100;

  @ViewChild(MatSort)
  private sort!: MatSort;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private csvService: CsvService) {}

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;

    if (this.bigData) this.dataSource.paginator = this.paginator;

    setTimeout(() => {
      const parsedCsv = this.csvService.parse(this.csv, {
        header: this.header,
        delimiter: this.delimiter,
        newline: this.endOfLine,
        escapeChar: this.escapeChar,
        quoteChar: this.quoteChar,
        comments: this.comments
      });

      this.dataSource.data = parsedCsv.data;
      this.columns = parsedCsv.meta.fields ?? [];
    }, 0);
  }
}
