import { Injectable } from '@angular/core';

interface Data {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  baseUrl = '{{test}}';
  httpOptions = { 'Content-Type': 'application/json' };

  public async tableData(id: string): Promise<Data> {
    const response = await fetch(this.baseUrl + '/' + id, {
      method: 'GET',
      headers: this.httpOptions
    });

    return await response.json();
  }
}
