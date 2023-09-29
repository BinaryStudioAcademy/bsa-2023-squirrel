import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserPredicates } from '@shared/helpers/user-predicates';

import { Branch } from 'src/app/models/branch/branch';
import { Comment } from 'src/app/models/comment/comment';
import { PullRequest } from 'src/app/models/pull-request/pull-request';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-pull-request-list',
    templateUrl: './pull-request-list.component.html',
    styleUrls: ['./pull-request-list.component.sass'],
})
export class PullRequestListComponent {
    public dropdownItems: string[];

    public dropdownAuthors: UserDto[];

    public pullRequests: PullRequest[];

    public searchForm: FormGroup = new FormGroup({});

    constructor(private fb: FormBuilder) {
        this.dropdownItems = this.getBranchTypes();
        this.searchForm = this.fb.group({
            search: ['', []],
        });

        this.pullRequests = this.getPullRequests();
        this.dropdownAuthors = this.getAuthors();
    }

    getBranchTypes() {
        // TODO: fetch data from server, remove placeholder data
        return ['All', 'Open', 'Declined', 'Merged'];
    }

    getAuthors() {
        // TODO: fetch data from server, remove placeholder data
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'John',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;
        const user2 = {
            id: 2,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'Test',
            lastName: 'Smith',
            userName: '',
        } as UserDto;
        const user3 = {
            id: 3,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'Test',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;

        return [user, user2, user3];
    }

    getFullName(item: UserDto) {
        return `${item.firstName} ${item.lastName} ${item.userName ? `(${item.userName})` : ''}`;
    }

    getPullRequests() {
        // TODO: fetch data from server, remove placeholder data
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'John',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;
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

        return [pullRequest, pullRequest, pullRequest, pullRequest, pullRequest, pullRequest, pullRequest];
    }

    filter(item: any, value: string) {
        return UserPredicates.findByFullNameOrUsernameOrEmail(item, value);
    }

    onAuthorSelectionChange($event: any) {
        // TODO: add filter logic, remove log
        // eslint-disable-next-line no-console
        console.log($event);
    }

    onBranchTypeSelectionChange($event: any) {
        // TODO: add filter logic, remove log
        // eslint-disable-next-line no-console
        console.log($event);
    }
}
