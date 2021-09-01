import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { Subject } from 'rxjs';

@Injectable()
export class FaPaginator implements MatPaginatorIntl {
  changes = new Subject<void>();

  firstPageLabel = 'صفحه اول';
  itemsPerPageLabel = 'تعداد سطر در هر صفحه:';
  lastPageLabel = 'صفحه آخر';

  nextPageLabel = 'صفحه بعد';
  previousPageLabel = 'صفحه قبل';

  getRangeLabel(page: number, pageSize: number, length: number): string {
    if (length === 0) return 'صفحه 1 از 1';

    const amountPages = Math.ceil(length / pageSize);
    return `صفحه ${page + 1} از ${amountPages}`;
  }
}
