import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedModule } from '@shared/shared.module';

import { UserProfileComponent } from './user-profile-page/user-profile.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';

@NgModule({
    declarations: [UserProfileComponent],
    imports: [SharedModule, UserProfileRoutingModule, FontAwesomeModule],
})
export class UserProfileModule {}
