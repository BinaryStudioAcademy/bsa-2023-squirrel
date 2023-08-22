import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';

import { ButtonComponent } from './components/button/button.component';
import { SidebarComponent } from '@shared/components/sidebar/sidebar.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { NotFoundComponent } from './components/not-found/not-found.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
        })],
    declarations: [LoadingSpinnerComponent, NotFoundComponent, ButtonComponent, SidebarComponent],
    exports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        LoadingSpinnerComponent,
        NotFoundComponent,
        ButtonComponent,
        SidebarComponent,
        ToastrModule,
    ],
})
export class SharedModule {}
