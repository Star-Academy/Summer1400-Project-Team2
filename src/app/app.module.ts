import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {DatasetsModule} from './pages/datasets/datasets.module';

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, DatasetsModule],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
