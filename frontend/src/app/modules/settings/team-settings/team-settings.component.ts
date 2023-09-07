import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { AddUserModalComponent } from '@modules/settings/add-user-modal/add-user-modal.component';

import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-team-settings-menu',
    templateUrl: './team-settings.component.html',
    styleUrls: ['./team-settings.component.sass'],
})
export class TeamSettingsComponent extends BaseComponent implements OnInit {
    public users: UserDto[];

    constructor(
        public dialog: MatDialog,
        private spinner: SpinnerService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.users = this.getUsers();
    }

    public OpenAddUserModal(): void {
        const dialogRef = this.dialog.open(AddUserModalComponent, {
            width: '400px',
            height: '400px',
        });

        /*dialogRef.componentInstance.projectCreated.subscribe(() => this.loadProjects());*/
    }

    getUsers() {
        /*this.spinner.show();
        this.projectService.getProjectUsers('1')
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe({
                error: err => {
                    this.notificationService.error(err.message);
                },
                next: projectUsers => {
                    this.users = projectUsers;
                },
            });*/
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
}
