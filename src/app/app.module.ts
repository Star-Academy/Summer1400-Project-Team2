import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProcessorModalComponent } from './components/modals/processor-modal/processor-modal.component';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { PipelineModule } from './pages/pipeline/pipeline.module';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    ProcessorModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatDialogModule,
     PipelineModule, 
     FormsModule
  ],
  providers: [MatDialog],
  bootstrap: [AppComponent]
})
export class AppModule {}
