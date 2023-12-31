import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { DatabaseService } from '@core/services/database.service';
import { NotificationService } from '@core/services/notification.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { TablesService } from '@core/services/tables.service';
import { CreateDbModalComponent } from '@modules/main/create-db-modal/create-db-modal.component';
import { filter, take, takeUntil } from 'rxjs';

import { DatabaseDto } from 'src/app/models/database/database-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';
import { QueryParameters } from 'src/app/models/sql-service/query-parameters';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent extends BaseComponent implements OnInit {
    public project: ProjectResponseDto;

    public selectedDbName: string;

    public dbNames: string[] = [];

    public databases: DatabaseDto[] = [];

    private currentDb: DatabaseDto;

    constructor(
        public dialog: MatDialog,
        private sharedProject: SharedProjectService,
        private databaseService: DatabaseService,
        private notificationService: NotificationService,
        private tableService: TablesService,
    ) {
        super();
    }

    public ngOnInit() {
        this.loadProject();
    }

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
        this.currentDb = this.databases!.find((database) => database.dbName === this.selectedDbName)!;

        this.selectDb(this.currentDb);
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateDbModalComponent, {
            width: '700px',
            maxHeight: '90%',
            data: {
                dbEngine: this.project.dbEngine,
                projectId: this.project.id,
            },
            autoFocus: false,
        });

        dialogRef.componentInstance.addedDatabase.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (addedDatabase: DatabaseDto) => {
                this.databases.push(addedDatabase);
                this.dbNames.push(addedDatabase.dbName);
            },
        });
    }

    private loadProject() {
        this.sharedProject.project$
            .pipe(
                filter((project) => project !== null),
                take(1),
            )
            .subscribe({
                next: (project) => {
                    if (project) {
                        this.project = project;
                        this.loadDatabases();
                    }
                },
            });
    }

    private loadDatabases() {
        this.databaseService
            .getAllDatabases(this.project.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (databases) => {
                    this.databases = databases;
                    this.dbNames = databases.map((database) => database.dbName);
                },
            });
    }

    public selectDb(db: DatabaseDto) {
        this.currentDb = db;
        const query: QueryParameters = {
            clientId: this.currentDb.guid,
            filterSchema: '',
            filterName: '',
        };

        this.tableService
            .getAllTablesNames(query)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    this.notificationService.info('db has stable connection');
                    this.sharedProject.setCurrentDb(db);
                    this.selectedDbName = db.dbName;
                },
                error: () => {
                    this.notificationService.error('fail connect to db');
                },
            });
    }
}
