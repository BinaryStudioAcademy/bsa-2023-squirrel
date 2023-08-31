import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SolutionComponent } from './solution/solution.component';
import { ChangesRoutingModule } from './changes-routing.module';

@NgModule({
    declarations: [
        SolutionComponent,
    ],
    imports: [
        CommonModule,
        ChangesRoutingModule,
    ],
})
export class ChangesModule { }
