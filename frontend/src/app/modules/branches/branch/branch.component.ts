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
        const mDate = new Date(date);
        const time = mDate.getTime();

        const localDate = new Date(time - mDate.getTimezoneOffset() * 60 * 1000);

        return moment(localDate).startOf('seconds').fromNow();
    }

    public calculateFilling(): string {
        return `${(this.branch.behind / (this.branch.ahead + this.branch.behind)) * 100}%`;
    }
}
