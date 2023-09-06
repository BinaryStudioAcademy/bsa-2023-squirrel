import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ScriptsPageComponent } from './scripts-page/scripts-page.component';

const routes: Routes = [{ path: '', component: ScriptsPageComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ScriptsRoutingModule {}
