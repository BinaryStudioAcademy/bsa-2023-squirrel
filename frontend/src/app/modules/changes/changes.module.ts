import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

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
    ],
})
export class ChangesModule { }
