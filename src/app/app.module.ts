import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { PipelineModule } from './pages/pipeline/pipeline.module';
import { FormsModule } from '@angular/forms';
import { DatasetsModule } from './pages/datasets/datasets.module';


@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PipelineModule,
    DatasetsModule,
    MatDialogModule,
    FormsModule
  ],
  providers: [MatDialog],
  bootstrap: [AppComponent]
})
export class AppModule {}
