import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CodeComponent } from './code/code.component';

const routes: Routes = [{ path: '', component: CodeComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CodeRoutingModule {}
