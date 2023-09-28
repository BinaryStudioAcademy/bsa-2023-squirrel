import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { MaterialModule } from 'src/app/material/material.module';

import { BranchComponent } from './branch/branch.component';
import { BranchListComponent } from './branch-list/branch-list.component';
import { BranchesRoutingModule } from './branches-routing.module';

@NgModule({
    declarations: [
        BranchListComponent,
        BranchComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        BranchesRoutingModule,
        MaterialModule,
    ],
})
export class BranchesModule { }
