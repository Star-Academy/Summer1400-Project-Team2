import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnChanges {
  sourseCsv = '';
  targetCsv = '';

  @Input()
  nodeId!: number;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['nodeId']) console.log(changes['nodeId']);
  }
}
