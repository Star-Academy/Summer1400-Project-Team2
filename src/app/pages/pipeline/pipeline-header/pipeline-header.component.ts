import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ToastService } from '../services/toast.service';

@Component({
  selector: 'app-pipeline-header',
  templateUrl: './pipeline-header.component.html',
  styleUrls: ['./pipeline-header.component.scss']
})
export class PipelineHeaderComponent implements OnInit {
  @Input() pipelineId =0;
  constructor(private http:HttpClient,private toast:ToastService) {}
  ngOnInit(): void {
    this.getPipelineName(this.pipelineId).subscribe((res:any)=>{
      console.log(res,res.modelName);
      this.pipelineName = res.modelName;
      this.updatedName = res.modelName;      
    })
  }
  pipelineName ='نام پایپلاین';
  updatedName=this.pipelineName;
  hasEdited = false;
  isRunning=false;
  runImgSrc='../../../assets/images/icons/play-button.svg';

  onEditBtn() {
    this.hasEdited = true;
    this.toggleEdit();
  }
  toggleEdit() {
    const pipelineInfo = <HTMLElement>document.querySelector('.pipeline-info');
    const changeContainer = <HTMLElement>document.querySelector('.change-pipeline__container');
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
    console.log(this.pipelineName,this.updatedName);
    this.updatePipelineName(this.pipelineId,this.updatedName).subscribe(response=>{
      console.log(response);
      this.pipelineName = this.updatedName;
      this.toast.openSnackBar("تغییر نام با موفقیت انجام شد","Codestar");
      this.toggleEdit();
    })
  }
  onBackBtn(){
    window.history.back();
  }

  onUpdatedName(event:Event){
    this.updatedName = (<HTMLInputElement>event.target).value;
  }

  onRunBtn(){
    this.isRunning = !this.isRunning;
    if(this.isRunning){
      this.runImgSrc ='../../../assets/images/icons/stop-button.svg';
    }else{
      this.runImgSrc ='../../../assets/images/icons/play-button.svg';
    }
  }
  updatePipelineName(id:number,newName:string){
    console.log(id,newName);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':'application/json'        
      })
    };
   return this.http.put(`https://codestar.iran.liara.run/pipeline/editName/${id}`,{newName:newName},httpOptions);
  }
  getPipelineName(id:number){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':'application/json'        
      })
    };
    return this.http.get(`https://codestar.iran.liara.run/pipeline/name/${id}`,httpOptions);
  }
}
