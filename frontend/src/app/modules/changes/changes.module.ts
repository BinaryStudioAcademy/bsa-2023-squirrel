import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { ChangesComponent } from './changes/changes.component';
import { SolutionComponent } from './solution/solution.component';
import { ChangesRoutingModule } from './changes-routing.module';

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
