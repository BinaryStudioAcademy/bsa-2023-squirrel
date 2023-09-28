import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BranchListComponent } from './branch-list/branch-list.component';

const routes: Routes = [
    { path: '', component: BranchListComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [
        RouterModule,
    ],
})
export class BranchesRoutingModule { }
