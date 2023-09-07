import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { AngularDelegate, ModalController } from '@ionic/angular';
import { SharedModule } from '@shared/shared.module';

import { CreateBranchModalComponent } from './main-page/create-branch-modal/create-branch-modal.component';
import { MainHeaderComponent } from './main-page/headers/main-header/main-header.component';
import { NavbarHeaderComponent } from './main-page/headers/navbar-header/navbar-header.component';
import { MainComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';

@NgModule({
    declarations: [MainComponent, MainHeaderComponent, NavbarHeaderComponent, CreateBranchModalComponent],
    imports: [SharedModule, MainRoutingModule, MatSelectModule, MatButtonModule, MatDialogModule],
    providers: [ModalController, AngularDelegate],
})
export class MainModule {}
