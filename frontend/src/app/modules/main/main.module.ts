import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { TreeComponent } from '../../shared/components/tree/tree.component';

import { MainComponent } from './main-page/main-page.component';
import { MainRoutingModule } from './main-routing.module';

@NgModule({
    declarations: [MainComponent],
    imports: [SharedModule, MainRoutingModule, TreeComponent],
})
export class MainModule {}
