import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { LandingPageComponent } from './landing-page/landing-page.component';
import { LandingRoutingModule } from './landing-routing.module';

@NgModule({
    declarations: [LandingPageComponent],
    imports: [SharedModule, LandingRoutingModule],
})
export class LandingModule {}
