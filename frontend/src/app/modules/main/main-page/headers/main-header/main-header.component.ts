import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DatabaseService } from '@core/services/database.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { CreateDbModalComponent } from '@modules/main/create-db-modal/create-db-modal.component';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

import { DatabaseInfoDto } from '../../../../../models/database/database-info-dto';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent implements OnInit {
    @Output() choseDb = new EventEmitter<DatabaseInfoDto>();

    public project: ProjectResponseDto;

    public selectedDbName: string;

    public dbNames: string[] = [];

    public db: DatabaseInfoDto[] = [];

    constructor(
        private sharedProject: SharedProjectService,
        public dialog: MatDialog,
        private databaseService: DatabaseService,
        // eslint-disable-next-line no-empty-function
    ) {
    }

    ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        if (this.selectedDbName === value) {
            return;
        }
        this.selectedDbName = value;
        const db = this.db.find(x => x.dbName === value);

        if (db) {
            this.choseDb.emit(db);
        }
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateDbModalComponent, {
            width: '700px',
            data: {
                dbEngine: this.project.dbEngine,
                projectId: this.project.id,
            },
            autoFocus: false,
        });

        dialogRef.componentInstance.dbName.subscribe({
            next: (db: DatabaseInfoDto) => {
                this.dbNames.push(db.dbName);
            },
        });
    }

    private loadProject() {
        this.sharedProject.project$.subscribe({
            next: project => {
                if (project) {
                    this.project = project;
                    this.loadDatabases();
                }
            },
        });
    }

    private loadDatabases() {
        this.databaseService.getAllDatabases(this.project.id).subscribe({
            next: databases => {
                this.dbNames = databases.map(database => database.dbName);
                this.db = databases;
            },
        });
    }
}
