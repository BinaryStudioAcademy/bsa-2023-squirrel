import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '@core/base/base.component';
import { CommitChangesService } from '@core/services/commit-changes.service';
import { takeUntil } from 'rxjs';

import { TextPairDifferenceDto } from 'src/app/models/text-pair/text-pair-difference-dto';

import { DatabaseItemContentCompare } from '../../../models/database-items/database-item-content-compare';

@Component({
    selector: 'app-changes',
    templateUrl: './changes.component.html',
    styleUrls: ['./changes.component.sass'],
})
export class ChangesComponent extends BaseComponent implements OnInit {
    public textPair: TextPairDifferenceDto;

    contentChanges: DatabaseItemContentCompare[] = [];

    constructor(
        private commitChangesService: CommitChangesService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.commitChangesService.contentChanges$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((changes) => {
                this.contentChanges = changes;
            });
    }
}
