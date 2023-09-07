import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { SharedModule } from '@shared/shared.module';

import { MainHeaderComponent } from './main-page/headers/main-header/main-header.component';
import { NavbarHeaderComponent } from './main-page/headers/navbar-header/navbar-header.component';
import { MainComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';
import { CreateBranchModalComponent } from './main-page/create-branch-modal/create-branch-modal.component';

@NgModule({
    declarations: [MainComponent, MainHeaderComponent, NavbarHeaderComponent, CreateBranchModalComponent],
    imports: [SharedModule, MainRoutingModule, MatSelectModule, MatButtonModule],
})
export class MainModule {}
