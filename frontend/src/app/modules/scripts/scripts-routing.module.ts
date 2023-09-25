import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnsavedScriptGuard } from '@core/guards/unsaved-script.guard';

import { ScriptsPageComponent } from './scripts-page/scripts-page.component';

const routes: Routes = [{ path: '', component: ScriptsPageComponent, canDeactivate: [UnsavedScriptGuard] }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ScriptsRoutingModule {}
