import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from '@shared/shared.module';

import { MaterialModule } from './material/material.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, SharedModule, AppRoutingModule, MaterialModule],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
