import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MaterialModule } from 'src/app/material/material.module';

import { PullRequestComponent } from './pull-request/pull-request.component';
import { PullRequestListComponent } from './pull-request-list/pull-request-list.component';

@NgModule({
    declarations: [PullRequestListComponent, PullRequestComponent],
    imports: [CommonModule, MaterialModule],
    exports: [PullRequestComponent, PullRequestListComponent],
})
export class PullRequestModule {}
