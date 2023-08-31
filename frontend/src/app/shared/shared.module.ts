import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '@shared/components/sidebar/sidebar.component';
import { ToastrModule } from 'ngx-toastr';

import { MaterialModule } from '../material/material.module';

import { AvatarComponent } from './components/avatar/avatar.component';
import { BarrierComponent } from './components/barrier/barrier.component';
import { ButtonComponent } from './components/button/button.component';
import { ConfirmationModalComponent } from './components/confirmation-modal/confirmation-modal.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import { DropdownSelectComponent } from './components/dropdown-select/dropdown-select.component';
import { InputComponent } from './components/input/input.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ProfileMenuComponent } from './components/profile-menu/profile-menu.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        MaterialModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
        }),
        MatMenuModule,
    ],
    declarations: [
        LoadingSpinnerComponent,
        NotFoundComponent,
        ButtonComponent,
        SidebarComponent,
        InputComponent,
        BarrierComponent,
        ProfileMenuComponent,
        AvatarComponent,
        DropdownSelectComponent,
        ConfirmationModalComponent,
        DropdownComponent,
    ],
    exports: [
        CommonModule,
        RouterModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        LoadingSpinnerComponent,
        NotFoundComponent,
        ButtonComponent,
        SidebarComponent,
        DropdownSelectComponent,
        ToastrModule,
        InputComponent,
        BarrierComponent,
        ProfileMenuComponent,
        AvatarComponent,
        ConfirmationModalComponent,
        DropdownComponent,
    ],
})
export class SharedModule {}
