import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SolutionComponent } from '@modules/changes/solution/solution.component';

const routes: Routes = [
    { path: '', component: SolutionComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ChangesRoutingModule { }
