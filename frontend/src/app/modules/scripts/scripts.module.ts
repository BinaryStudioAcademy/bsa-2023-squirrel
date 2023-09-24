import { CdkListboxModule } from '@angular/cdk/listbox';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedModule } from '@shared/shared.module';

import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { CreateScriptModalComponent } from './create-script-modal/create-script-modal.component';
import { ScriptComponent } from './script/script.component';
import { ScriptErrorResultComponent } from './script-error-result/script-error-result.component';
import { ScriptResultComponent } from './script-result/script-result.component';
import { ScriptsPageComponent } from './scripts-page/scripts-page.component';
import { ScriptsRoutingModule } from './scripts-routing.module';

@NgModule({
    declarations: [
        ScriptComponent,
        ScriptsPageComponent,
        CreateScriptModalComponent,
        ScriptErrorResultComponent,
        ScriptResultComponent,
        ConfirmationDialogComponent,
    ],
    imports: [
        CommonModule,
        ScriptsRoutingModule,
        SharedModule,
        CdkListboxModule,
        FontAwesomeModule,
        MatDialogModule,
        MatButtonModule,
    ],
})
export class ScriptsModule {}
