import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ChangesComponent } from './changes/changes.component';
import { SolutionComponent } from './solution/solution.component';
import { ChangesRoutingModule } from './changes-routing.module';
import {SharedModule} from "@shared/shared.module";

@NgModule({
    declarations: [
        SolutionComponent,
        ChangesComponent,
    ],
    imports: [
        CommonModule,
        ChangesRoutingModule,
        SharedModule,
    ],
})
export class ChangesModule { }
