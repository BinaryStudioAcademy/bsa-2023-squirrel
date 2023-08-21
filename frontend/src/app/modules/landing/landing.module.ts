import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { LandingPageComponent } from './landing-page/landing-page.component';
import { LogoComponent } from './logo/logo.component';
import { RegistrationComponent } from './registration/registration.component';
import { LandingRoutingModule } from './landing-routing.module';

@NgModule({
    declarations: [LandingPageComponent, LogoComponent, RegistrationComponent],
    imports: [SharedModule, LandingRoutingModule],
})
export class LandingModule {}
