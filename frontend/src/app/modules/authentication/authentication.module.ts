import { NgModule } from '@angular/core';
import { AuthenticationPageComponent } from '@modules/authentication/authentication-page/authentication-page.component';
import { AuthenticationRoutingModule } from '@modules/authentication/authentication-routing.module';
import { SharedModule } from '@shared/shared.module';

import { LogoComponent } from './logo/logo.component';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
    declarations: [AuthenticationPageComponent, LogoComponent, RegistrationComponent],
    imports: [SharedModule, AuthenticationRoutingModule],
})
export class AuthenticationModule {}
