import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { CodemirrorModule } from '@ctrl/ngx-codemirror';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SidebarComponent } from '@shared/components/sidebar/sidebar.component';
import { ToastrModule } from 'ngx-toastr';

import { MaterialModule } from '../material/material.module';

import { AvatarComponent } from './components/avatar/avatar.component';
import { BarrierComponent } from './components/barrier/barrier.component';
import { ButtonComponent } from './components/button/button.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { CloseBtnComponent } from './components/close-btn/close-btn.component';
import { CodeComponent } from './components/code/code.component';
import { CodeEditorComponent } from './components/code-editor/code-editor.component';
import { ConfirmationModalComponent } from './components/confirmation-modal/confirmation-modal.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import { DropdownSelectComponent } from './components/dropdown-select/dropdown-select.component';
import { InfoTooltipComponent } from './components/info-tooltip/info-tooltip.component';
import { InputComponent } from './components/input/input.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ProfileMenuComponent } from './components/profile-menu/profile-menu.component';
import { TreeComponent } from './components/tree/tree.component';
import { TreeOneComponent } from './components/tree-one/tree-one.component';
import { EmailOverflowDirective } from './directives/email-overflow.directive';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        MaterialModule,
        FontAwesomeModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
        }),
        MatMenuModule,
        MatTooltipModule,
        CodemirrorModule,
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
        TreeComponent,
        TreeOneComponent,
        CheckboxComponent,
        EmailOverflowDirective,
        InfoTooltipComponent,
        CodeComponent,
        CloseBtnComponent,
        CodeEditorComponent,
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
        TreeComponent,
        TreeOneComponent,
        CheckboxComponent,
        EmailOverflowDirective,
        InfoTooltipComponent,
        CodeComponent,
        CloseBtnComponent,
        CodeEditorComponent,
    ],
})
export class SharedModule {}
