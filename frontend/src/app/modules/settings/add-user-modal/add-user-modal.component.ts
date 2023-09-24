import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { UserPredicates } from '@shared/helpers/user-predicates';
import { takeUntil } from 'rxjs';

import { ProjectResponseDto } from '../../../models/projects/project-response-dto';
import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-add-user-modal',
    templateUrl: './add-user-modal.component.html',
    styleUrls: ['./add-user-modal.component.sass'],
})
export class AddUserModalComponent extends BaseComponent implements OnInit {
    @Output() public userAdded = new EventEmitter<UserDto[]>();

    public dropdownUsers: UserDto[];

    public selectedUsers: UserDto[] = [];

    public isDropdownVisible = false;

    public project: ProjectResponseDto;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: { users: UserDto[] },
        public dialogRef: MatDialogRef<AddUserModalComponent>,
        private sharedProjectService: SharedProjectService,
        private userService: UserService,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
    ) {
        super();
    }

    public getFullName(item: UserDto) {
        return `${item.firstName} ${item.lastName} ${item.userName ? `(${item.userName})` : ''}`;
    }

    public filter(item: any, value: string) {
        return UserPredicates.findByFullNameOrUsernameOrEmail(item, value);
    }

    public addUser(): void {
        this.projectService
            .addUsersToProject(this.project.id, this.selectedUsers)
            .pipe(
                takeUntil(this.unsubscribe$),
            )
            .subscribe({
                next: () => {
                    this.dialogRef.close(this.selectedUsers);
                    this.notificationService.info('Users added successfully');
                    this.userAdded.emit(this.selectedUsers);
                },
                error: () => {
                    this.notificationService.error('Failed to added users');
                },
            });
    }

    public close(): void {
        this.dialogRef.close();
    }

    public onDropdownValueChange(selectedItems: UserDto[]): void {
        this.selectedUsers = selectedItems;
    }

    public ngOnInit(): void {
        this.userService
            .getAllUsers()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                {
                    next: (users: UserDto[]) => {
                        this.dropdownUsers = users.filter(user => !this.data.users.some(u => u.id === user.id));
                        this.isDropdownVisible = true;
                    },
                    error: () => {
                        this.notificationService.error('Failed to load users');
                    },
                },
            );
        this.sharedProjectService.project$.subscribe({
            next: project => {
                if (project) {
                    this.project = project;
                }
            },
        });
    }
}
