import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '../../shared/shared.module';

import { DownloadsComponent } from './downloads-page/downloads.component';
import { DownloadsRoutingModule } from './downloads-routing.module';

@NgModule({
    declarations: [DownloadsComponent],
    imports: [CommonModule, DownloadsRoutingModule, FontAwesomeModule, SharedModule],
})
export class DownloadsModule {}
