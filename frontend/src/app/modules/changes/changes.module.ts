import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { ChangesComponent } from './changes/changes.component';
import { ChangesRoutingModule } from './changes-routing.module';

@NgModule({
    declarations: [
        ChangesComponent,
    ],
    imports: [
        CommonModule,
        ChangesRoutingModule,
        SharedModule,
    ],
})
export class ChangesModule { }
