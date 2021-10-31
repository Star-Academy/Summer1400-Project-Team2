import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';

import { CsvTableComponent } from './csv-table.component';
import { CsvService } from 'src/app/services/csv/csv.service';
import { FaPaginatorModule } from '../fa-paginator/fa-paginator.module';

@NgModule({
  declarations: [CsvTableComponent],
  imports: [CommonModule, MatTableModule, MatSortModule, FaPaginatorModule],
  providers: [CsvService],
  exports: [CsvTableComponent]
})
export class CsvTableModule {}
