import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '@core/core.module';
import { CodemirrorModule } from '@ctrl/ngx-codemirror';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthenticationModule } from '@modules/authentication/authentication.module';
import { DownloadsModule } from '@modules/downloads/downloads.module';
import { UserProfileModule } from '@modules/user-profile/user-profile.module';
import { SharedModule } from '@shared/shared.module';

import { MaterialModule } from './material/material.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    declarations: [AppComponent],
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
        UserProfileModule,
        FontAwesomeModule,
        DownloadsModule,
        CodemirrorModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
