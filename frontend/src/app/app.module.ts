import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '@core/core.module';
import { AuthenticationModule } from '@modules/authentication/authentication.module';
import { SharedModule } from '@shared/shared.module';

import { MaterialModule } from './material/material.module';
import { SwaggerComponent } from './testing/swagger/swagger.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [AppComponent, SwaggerComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        SharedModule,
        AppRoutingModule,
        MaterialModule,
        ReactiveFormsModule,
        CoreModule,
        HttpClientModule,
        AuthenticationModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
