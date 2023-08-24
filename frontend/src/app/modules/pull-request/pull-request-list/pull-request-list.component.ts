import { Component, Input, OnInit } from '@angular/core';

import { Branch } from 'src/app/models/branch/branch';
import { Comment } from 'src/app/models/comment/comment';
import { PullRequest } from 'src/app/models/pull-request/pull-request';
import { User } from 'src/app/models/user/user';

@Component({
    selector: 'app-pull-request-list',
    templateUrl: './pull-request-list.component.html',
    styleUrls: ['./pull-request-list.component.sass'],
})
export class PullRequestListComponent implements OnInit {
    @Input() public pullRequests: PullRequest[];

    constructor() {
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

        const pullRequest = {
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

        this.pullRequests = [pullRequest, pullRequest, pullRequest, pullRequest, pullRequest, pullRequest, pullRequest];
    }

    ngOnInit(): void {}
}
