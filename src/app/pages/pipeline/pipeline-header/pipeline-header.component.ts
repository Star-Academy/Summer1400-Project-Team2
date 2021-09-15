import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-pipeline-header',
  templateUrl: './pipeline-header.component.html',
  styleUrls: ['./pipeline-header.component.scss']
})
export class PipelineHeaderComponent {
  @Output()
  toggleDetail = new EventEmitter<void>();

  pipelineName = 'نام پایپلاین';
  updatedName = this.pipelineName;
  hasEdited = false;
  isRunning = false;
  runImgSrc = '../../../assets/images/icons/play-button.svg';

  onEditBtn() {
    this.hasEdited = true;
    this.toggleEdit();
  }
  toggleEdit() {
    const pipelineInfo = <HTMLElement>document.querySelector('.pipeline-info');
    const changeContainer = <HTMLElement>(
      document.querySelector('.change-pipeline__container')
    );
    if (this.hasEdited) {
      pipelineInfo.style.display = 'none';
      changeContainer.style.display = 'flex';
    } else {
      pipelineInfo.style.display = 'flex';
      changeContainer.style.display = 'none';
    }
  }

  onCancelBtn() {
    this.hasEdited = false;
    this.updatedName = this.pipelineName;
    this.toggleEdit();
  }
  onSubmitBtn() {
    this.hasEdited = false;
    this.pipelineName = this.updatedName;
    this.toggleEdit();
  }
  onBackBtn() {
    window.history.back();
  }

  onUpdatedName(event: Event) {
    this.updatedName = (<HTMLInputElement>event.target).value;
  }

  onRunBtn() {
    this.isRunning = !this.isRunning;
    if (this.isRunning) {
      this.runImgSrc = '../../../assets/images/icons/stop-button.svg';
    } else {
      this.runImgSrc = '../../../assets/images/icons/play-button.svg';
    }
  }
}
