import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { UserPredicates } from '@shared/helpers/user-predicates';

import { DbEngine } from '../../../models/projects/db-engine';
import { ProjectDto } from '../../../models/projects/project-dto';
import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-add-user-modal',
    templateUrl: './add-user-modal.component.html',
    styleUrls: ['./add-user-modal.component.sass'],
})
export class AddUserModalComponent extends BaseComponent implements OnInit {
    @Output() public userAdded = new EventEmitter<ProjectDto>();

    public dropdownUsers: UserDto[];

    public selectedEngine: DbEngine;

    constructor(
        public dialogRef: MatDialogRef<AddUserModalComponent>,
        private userService: UserService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    ngOnInit() {
        this.dropdownUsers = this.getUsers();
    }

    getUsers() {
        const user = {
            id: 1,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'Rome',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;
        const user2 = {
            id: 2,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'Tom',
            lastName: 'Smith',
            userName: '',
        } as UserDto;
        const user3 = {
            id: 3,
            avatarUrl: 'https://picsum.photos/200',
            email: 'test@test.test',
            firstName: 'Kris',
            lastName: 'Smith',
            userName: 'Johnny',
        } as UserDto;

        return [user, user2, user3];
    }

    getFullName(item: UserDto) {
        return `${item.firstName} ${item.lastName} ${item.userName ? `(${item.userName})` : ''}`;
    }

    filter(item: any, value: string) {
        return UserPredicates.findByFullNameOrUsernameOrEmail(item, value);
    }

    public addUser(): void {

    }

    onAuthorSelectionChange($event: any) {
        // TODO: add filter logic, remove log
        // eslint-disable-next-line no-console
        console.log($event);
    }

    public close(): void {
        this.dialogRef.close();
    }
}
