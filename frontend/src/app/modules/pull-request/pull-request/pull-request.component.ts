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

    constructor(private timeService: TimeSpanStringifierService) {
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'John',
            lastName: 'Smith',
            userName: 'Johnny',
        } as User;
        const branch = {
            id: 1,
            name: 'DEV',
        } as Branch;
        const date = new Date(1478708162000);
        const sourceBranch = {
            id: 1,
            name: 'bug/1191-status-delete',
        } as Branch;

        this.pullRequest = {
            id: 1,
            author: user,
            comments: [
                {
                    id: 1,
                    author: user,
                    content: 'test',
                    createdAt: date,
                    updatedAt: date,
                } as Comment,
            ],
            sourceBranch,
            destinationBranch: branch,
            title: '#1111 Fix random DROP DATABASE execution',
            createdAt: date,
            updatedAt: date,
            reviewers: [user, user, user, user],
        } as PullRequest;
    }

    public calculateTime(date: Date): string {
        return this.timeService.stringify(date);
    }
}
