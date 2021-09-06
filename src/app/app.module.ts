import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { PipelineModule } from './pages/pipeline/pipeline.module';
import { FormsModule } from '@angular/forms';
import { DatasetsModule } from './pages/datasets/datasets.module';
import { LoginModule } from './pages/login/login.module';
import { RegisterModule } from './pages/register/register.module';


@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PipelineModule,
    DatasetsModule,
    LoginModule,
    RegisterModule,
    MatDialogModule,
    FormsModule
  ],
  providers: [MatDialog],
  bootstrap: [AppComponent]
})
export class AppModule {}
