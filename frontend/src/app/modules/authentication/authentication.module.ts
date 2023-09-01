import { NgModule } from '@angular/core';
import { AuthenticationPageComponent } from '@modules/authentication/authentication-page/authentication-page.component';
import { AuthenticationRoutingModule } from '@modules/authentication/authentication-routing.module';
import { SharedModule } from '@shared/shared.module';

import { GoogleButtonComponent } from './google-button/google-button.component';
import { LoginComponent } from './login/login.component';
import { LogoComponent } from './logo/logo.component';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
    declarations: [
        AuthenticationPageComponent,
        LogoComponent,
        RegistrationComponent,
        LoginComponent,
        GoogleButtonComponent,
    ],
    imports: [SharedModule, AuthenticationRoutingModule],
})
export class AuthenticationModule {}
