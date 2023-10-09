import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { CodeComponent } from './code/code.component';
import { CodeRoutingModule } from './code-routing.module';

@NgModule({
    declarations: [CodeComponent],
    imports: [CommonModule, CodeRoutingModule, SharedModule],
})
export class CodeModule {}
