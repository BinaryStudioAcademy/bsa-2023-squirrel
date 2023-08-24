import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '@shared/components/sidebar/sidebar.component';

import { ButtonComponent } from './components/button/button.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TreeComponent } from './components/tree/tree.component';

@NgModule({
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
    ],
    imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule, RouterModule, TreeComponent],
})
export class SharedModule {}
