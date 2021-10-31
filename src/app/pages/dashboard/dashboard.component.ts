import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  innerWidth: number;

  constructor() {
    this.innerWidth = window.screen.width;
  }

  public width(): boolean {
    return innerWidth >= 480;
  }
}
