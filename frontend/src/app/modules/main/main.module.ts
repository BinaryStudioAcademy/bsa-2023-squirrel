import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { AngularDelegate, ModalController } from '@ionic/angular';
import { SharedModule } from '@shared/shared.module';

import { MainHeaderComponent } from './main-page/headers/main-header/main-header.component';
import { MainComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';

@NgModule({
    declarations: [MainComponent, MainHeaderComponent],
    imports: [SharedModule, MainRoutingModule, MatSelectModule, MatButtonModule, MatDialogModule],
    providers: [ModalController, AngularDelegate],
})
export class MainModule {}
