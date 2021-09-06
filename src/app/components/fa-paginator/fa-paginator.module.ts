import { NgModule } from '@angular/core';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { FaPaginator } from './fa-paginator.component';

@NgModule({
  imports: [MatPaginatorModule],
  declarations: [],
  providers: [{ provide: MatPaginatorIntl, useClass: FaPaginator }],
  exports: [MatPaginatorModule]
})
export class FaPaginatorModule {}
