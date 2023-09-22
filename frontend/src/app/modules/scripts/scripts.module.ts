import { CdkListboxModule } from '@angular/cdk/listbox';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { CreateScriptModalComponent } from './create-script-modal/create-script-modal.component';
import { ScriptComponent } from './script/script.component';
import { ScriptErrorResultComponent } from './script-error-result/script-error-result.component';
import { ScriptsPageComponent } from './scripts-page/scripts-page.component';
import { ScriptsRoutingModule } from './scripts-routing.module';

@NgModule({
    declarations: [ScriptComponent, ScriptsPageComponent, CreateScriptModalComponent, ScriptErrorResultComponent],
    imports: [CommonModule, ScriptsRoutingModule, SharedModule, CdkListboxModule],
})
export class ScriptsModule {}
