import { Component, Input } from '@angular/core';
import * as moment from 'moment';

import { BranchDetailsDto } from 'src/app/models/branch/branch-details-dto';

@Component({
    selector: 'app-branch',
    templateUrl: './branch.component.html',
    styleUrls: ['./branch.component.sass'],
})
export class BranchComponent {
    @Input() public branch: BranchDetailsDto;

    public calculateTime(date: Date): string {
        return moment(date).startOf('seconds').fromNow();
    }

    public calculateFilling(): string {
        return `${(this.branch.behind / (this.branch.ahead + this.branch.behind)) * 100}%`;
    }
}
