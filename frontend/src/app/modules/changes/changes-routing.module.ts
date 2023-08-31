import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangesComponent } from '@modules/changes/changes/changes.component';

const routes: Routes = [
    { path: '', component: ChangesComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ChangesRoutingModule { }
