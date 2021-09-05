import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PipelineModule } from './pages/pipeline/pipeline.module';
import { DatasetsModule } from './pages/datasets/datasets.module';
import { RegisterComponent } from './pages/register/register.component';
import { LoginModule } from './pages/login/login.module';

@NgModule({
  declarations: [AppComponent, RegisterComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PipelineModule,
    DatasetsModule,
    LoginModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
