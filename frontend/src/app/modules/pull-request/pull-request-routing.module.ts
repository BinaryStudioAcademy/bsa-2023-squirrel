import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PullRequestListComponent } from '@modules/pull-request/pull-request-list/pull-request-list.component';

const routes: Routes = [
    { path: '', component: PullRequestListComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [
        RouterModule,
    ],
})
export class PullRequestRoutingModule { }
