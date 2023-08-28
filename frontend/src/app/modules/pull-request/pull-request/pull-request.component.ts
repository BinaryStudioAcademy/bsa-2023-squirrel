import { Component, Input } from '@angular/core';
import { TimeSpanStringifierService } from '@core/services/time-span-stringifier.service';

import { Branch } from 'src/app/models/branch/branch';
import { Comment } from 'src/app/models/comment/comment';
import { PullRequest } from 'src/app/models/pull-request/pull-request';
import { User } from 'src/app/models/user/user';

@Component({
    selector: 'app-pull-request',
    templateUrl: './pull-request.component.html',
    styleUrls: ['./pull-request.component.sass'],
})
export class PullRequestComponent {
    @Input() public pullRequest: PullRequest;

    // eslint-disable-next-line no-empty-function
    constructor(private timeService: TimeSpanStringifierService) {}

    public calculateTime(date: Date): string {
        return this.timeService.stringify(date);
    }
}
