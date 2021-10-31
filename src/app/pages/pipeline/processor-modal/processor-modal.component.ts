import { Component, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PipelinePage } from '../pipeline.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-processor-modal',
  templateUrl: './processor-modal.component.html',
  styleUrls: ['./processor-modal.component.scss']
})
export class ProcessorModalComponent implements OnInit {
  action = '';
  constructor(
    public _dialog: MatDialog,
    public dialogRef: MatDialogRef<PipelinePage>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {}
  onFilterBtn() {
    this.action = 'filter';
    this.onClose();
  }
  onAggregateBtn() {
    this.action = 'aggregate';
    this.onClose();
  }
  onJoinBtn() {
    this.action = 'join';
    this.onClose();
  }
  onClose() {
    this.dialogRef.close({ event: this.action });
  }
}
