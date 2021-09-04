import { Component, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PipelinePage } from '../pipeline.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

export interface UsersData {
  name: string;
  id: number;
}
@Component({
  selector: 'app-processor-modal',
  templateUrl: './processor-modal.component.html',
  styleUrls: ['./processor-modal.component.scss']
})
export class ProcessorModalComponent implements OnInit {
  chosenProcessor = '';
  local_data: any;
  action = '';
  constructor(
    public _dialog: MatDialog,
    public dialogRef: MatDialogRef<PipelinePage>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    console.log(data);
    this.local_data = { ...data };
    this.action = this.local_data.action;
  }

  public openDialog(processor: any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      processor: processor
    };
    this._dialog.open(ProcessorModalComponent, dialogConfig);
  }

  ngOnInit(): void {}
  onFilterBtn() {
    this.action = 'filter';
    this.onClose();
    // this.dialogRef.close();
  }
  onAggregateBtn() {
    this.action = 'aggregate';
    this.onClose();
    // this.dialogRef.close();
  }
  onJoinBtn() {
    this.action = 'join';
    this.onClose();
    // this.dialogRef.close();
  }
  onClose() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
}
