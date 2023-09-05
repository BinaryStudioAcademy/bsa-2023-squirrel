import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';

import { ProjectDto } from '../../../../../models/projects/project-dto';
import {CreateDbModalComponent} from "@modules/main/create-db-modal/create-db-modal.component";

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent {
    public projectName: string = 'Project Name';

    public selectedDbName: string;

    public dbNames: string[] = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];

    constructor(public dialog: MatDialog) {
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }

    public openCreateModal(): void {
        this.dialog.open(CreateDbModalComponent, {
            width: '500px',
            height: '45%',
        });
    }
}
