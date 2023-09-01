import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { MaterialModule } from 'src/app/material/material.module';

import { PullRequestComponent } from './pull-request/pull-request.component';
import { PullRequestListComponent } from './pull-request-list/pull-request-list.component';
import { PullRequestRoutingModule } from './pull-request-routing.module';

@NgModule({
    declarations: [PullRequestListComponent, PullRequestComponent],
    imports: [CommonModule, MaterialModule, SharedModule, PullRequestRoutingModule],
})
export class PullRequestModule {}
