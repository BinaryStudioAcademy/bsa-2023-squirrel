import { Component, Input } from '@angular/core';
import * as moment from 'moment';

import { PullRequest } from 'src/app/models/pull-request/pull-request';

@Component({
    selector: 'app-pull-request',
    templateUrl: './pull-request.component.html',
    styleUrls: ['./pull-request.component.sass'],
})
export class PullRequestComponent {
    @Input() public pullRequest: PullRequest;

    public calculateTime(date: Date): string {
        return moment(date).startOf('seconds').fromNow();
    }
}
