import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, observable} from "rxjs";
import {tableData} from "../../pages/dashboard/tableData";

interface Data {
  name: string,
  id: string,
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  baseUrl = 'http://codestar.iran.liara.run/';
  httpOptions = {'Content-Type': 'application/json'};

  constructor(public http: HttpClient) {
  }

  public async tableData(id: string) {
    console.log('********')
    const response = await fetch(this.baseUrl + id, {
      method: 'GET',
      headers: this.httpOptions
    });
    console.log(response)
    console.log('********')
    return await response.json();
  }

  tableData1(id: string): Observable<tableData[]> {
    console.log('********')
    return this.http.get<tableData[]>(this.baseUrl + id);
  }

  public async create(id: string, name: string, entryDB: string, finalDB: string) {
    let body = {name: name, entryDB: entryDB, finalDB: finalDB}
    const response = await fetch(this.baseUrl + id, {
      method: 'POST',
      headers: this.httpOptions,
      body: JSON.stringify(body)
    });

    console.log(response);
    console.log(response.status);
  }
}
