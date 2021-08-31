import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pipeline-header',
  templateUrl: './pipeline-header.component.html',
  styleUrls: ['./pipeline-header.component.scss']
})
export class PipelineHeaderComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
  hasClicked = false;

  onEditBtn() {
    this.hasClicked = true;
    this.toggleEdit();
  }
  toggleEdit() {
    const pipelineInfo = <HTMLElement>document.querySelector('.pipeline-info');
    const changeContainer = <HTMLElement>document.querySelector('.change-pipeline__container');
    if (this.hasClicked) {
      pipelineInfo.style.display = 'none';
      changeContainer.style.display = 'flex';
    } else {
      pipelineInfo.style.display = 'flex';
      changeContainer.style.display = 'none';
    }
  }

  onCancelBtn() {
    this.hasClicked = false;
    this.toggleEdit();
  }
  onSubmitBtn() {
    this.hasClicked = false;
    this.toggleEdit();
  }
  onBackBtn(){
    window.history.back();
  }

}
