import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { AddUserModalComponent } from '@modules/settings/add-user-modal/add-user-modal.component';
import { UserPredicates } from '@shared/helpers/user-predicates';
import { finalize, takeUntil } from 'rxjs';

import { ProjectResponseDto } from '../../../models/projects/project-response-dto';
import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-team-settings-menu',
    templateUrl: './team-settings.component.html',
    styleUrls: ['./team-settings.component.sass'],
})
export class TeamSettingsComponent extends BaseComponent implements OnInit {
    public users: UserDto[];

    public project: ProjectResponseDto;

    public searchForm: FormGroup = new FormGroup({});

    public currentProjectOwner: UserDto | undefined;

    constructor(
        public dialog: MatDialog,
        private sharedProjectService: SharedProjectService,
        private spinner: SpinnerService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private fb: FormBuilder,
        private authService: AuthService,
    ) {
        super();

        this.searchForm = this.fb.group({
            search: ['', []],
        });
    }

    public ngOnInit(): void {
        this.sharedProjectService.project$.subscribe({
            next: (project) => {
                if (project) {
                    this.project = project;
                    this.getUsers();
                }
            },
        });

        this.authService
            .getUser()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((user) => {
                this.currentProjectOwner = user;
            });
    }

    public openAddUserModal(): void {
        const dialogRef = this.dialog.open(AddUserModalComponent, {
            width: '500px',
            height: '50%',
            data: { users: this.users },
        });

        dialogRef.componentInstance.userAdded.subscribe(() => this.getUsers());
    }

    private getUsers() {
        this.spinner.show();
        this.projectService
            .getProjectUsers(this.project.id)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe({
                error: (err) => {
                    this.notificationService.error(err.message);
                },
                next: (projectUsers) => {
                    this.users = projectUsers;
                },
            });
    }

    private filter(item: UserDto, value: string) {
        return UserPredicates.findByFullNameOrUsernameOrEmail(item, value);
    }

    public filterOptions(): UserDto[] {
        return this.users.filter((option) => this.filter?.call(this, option, this.searchForm.get('search')!.value));
    }

    public removeUser(userToRemove: UserDto): void {
        this.projectService
            .removeUserFromProject(this.project.id, userToRemove)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    this.notificationService.info('User removed successfully');
                    this.users = this.users.filter((user) => user.id !== userToRemove.id);
                    this.filterOptions();
                },
                error: () => {
                    this.notificationService.error('Failed to remove user');
                },
            });
    }
}
