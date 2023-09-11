import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AddUserModalComponent } from '@modules/settings/add-user-modal/add-user-modal.component';
import { GeneralSettingsComponent } from '@modules/settings/general-settings/general-settings.component';
import { SettingsMenuComponent } from '@modules/settings/settings-menu/settings-menu.component';
import { SettingsPageComponent } from '@modules/settings/settings-page/settings-page.component';
import { SettingsRoutingModule } from '@modules/settings/settings-routing.model';
import { TeamSettingsComponent } from '@modules/settings/team-settings/team-settings.component';
import { UserCardComponent } from '@modules/settings/user-card/user-card.component';
import { SharedModule } from '@shared/shared.module';

@NgModule({
    declarations: [
        TeamSettingsComponent,
        GeneralSettingsComponent,
        SettingsPageComponent,
        SettingsMenuComponent,
        UserCardComponent,
        AddUserModalComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        SettingsRoutingModule,
    ],
})
export class SettingsModule { }
