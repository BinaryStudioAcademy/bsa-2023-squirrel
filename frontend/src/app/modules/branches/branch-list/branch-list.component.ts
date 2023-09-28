import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { BranchDetailsDto } from 'src/app/models/branch/branch-details-dto';
import { UserDto } from 'src/app/models/user/user-dto';

@Component({
    selector: 'app-branch-list',
    templateUrl: './branch-list.component.html',
    styleUrls: ['./branch-list.component.sass'],
})
export class BranchListComponent {
    public dropdownItems: string[];

    public branches: BranchDetailsDto[];

    public searchForm: FormGroup = new FormGroup({});

    constructor(private fb: FormBuilder) {
        this.dropdownItems = this.getBranchTypes();
        this.searchForm = this.fb.group({
            search: ['', []],
        });

        this.branches = this.getBranches();
    }

    getBranchTypes() {
        // TODO: fetch data from server, remove placeholder data
        return ['All', 'Open', 'Merged'];
    }

    onBranchTypeSelectionChange($event: any) {
        // TODO: add filter logic
        // eslint-disable-next-line no-console
        console.log($event);
    }

    getBranches() {
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'John',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;
        const date = new Date(1478708162000);
        const branches = [];

        for (let i = 0; i < 15; i++) {
            const branch = {
                name: 'task/fix-empty-list-generation',
                isActive: true,
                lastUpdatedBy: user,
                ahead: Number((Math.random() * 5).toFixed(0)),
                behind: Number((Math.random() * 5).toFixed(0)),
                createdAt: date,
                updatedAt: date,
            } as BranchDetailsDto;

            branches.push(branch);
        }

        return branches;
    }
}
