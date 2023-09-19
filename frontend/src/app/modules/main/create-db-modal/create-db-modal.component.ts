import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { ConsoleConnectService } from '@core/services/console-connect.service';
import { DatabaseService } from '@core/services/database.service';
import { NotificationService } from '@core/services/notification.service';
import { SqlService } from '@core/services/sql.service';
import { takeUntil } from 'rxjs';

import { DatabaseDto } from 'src/app/models/database/database-dto';

import { DbConnection } from '../../../models/console/db-connection';
import { DbConnectionRemote } from '../../../models/console/db-connection-remote';
import { NewDatabaseDto } from '../../../models/database/new-database-dto';

@Component({
    selector: 'app-create-db-modal',
    templateUrl: './create-db-modal.component.html',
    styleUrls: ['./create-db-modal.component.sass'],
})
export class CreateDbModalComponent extends BaseComponent implements OnInit {
    @Output() public addedDatabase = new EventEmitter<DatabaseDto>();

    public dbForm: FormGroup = new FormGroup({});

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        private fb: FormBuilder,
        private consoleConnectService: ConsoleConnectService,
        private databaseService: DatabaseService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<CreateDbModalComponent>,
        private sqlService: SqlService,
    ) {
        super();
    }

    public ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.dbForm = this.fb.group({
            dbName: ['', Validators.required],
            serverName: ['', Validators.required],
            port: [''],
            username: [''],
            password: [''],
            localhost: [false],
            guid: ['', this.getValidators()],
        });
    }

    public addDataBase() {
        const connect: DbConnection = {
            dbName: this.dbForm.value.dbName,
            serverName: this.dbForm.value.serverName,
            port: +this.dbForm.value.port,
            username: this.dbForm.value.username,
            password: this.dbForm.value.password,
            dbEngine: this.data.dbEngine,
            isLocalhost: this.dbForm.value.localhost,
        };

        this.consoleConnectService.connect(connect)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: guid => {
                    this.saveDb(guid);
                },
                error: () => {
                    this.notificationService.error('Failed to connect to database');
                },
            });
    }

    public addDataBaseRemote() {
        const connect: DbConnectionRemote = {
            dbConnection: {
                dbName: this.dbForm.value.dbName,
                serverName: this.dbForm.value.serverName,
                port: +this.dbForm.value.port,
                username: this.dbForm.value.username,
                password: this.dbForm.value.password,
                dbEngine: this.data.dbEngine,
                isLocalhost: this.dbForm.value.localhost,
            },
            clientId: this.dbForm.value.guid,
        };

        this.sqlService.remoteConnect(connect)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    this.saveDb(connect.clientId);
                },
                error: () => {
                    this.notificationService.error('Failed to connect to database');
                },
            });
    }

    public changeLocalHost() {
        this.dbForm.get('guid')?.setValidators(this.getValidators());
        this.dbForm.get('guid')?.updateValueAndValidity();
    }

    private getValidators() {
        if (this.dbForm.value.localhost) {
            return null;
        }

        return Validators.required;
    }

    private saveDb(guid: string) {
        const database: NewDatabaseDto = {
            projectId: this.data.projectId,
            dbName: this.dbForm.value.dbName,
            guid,
        };

        this.databaseService.addDatabase(database)
            .subscribe({
                next: () => {
                    this.notificationService.info('database was successfully added');
                    this.addedDatabase.emit(database);
                    this.close();
                },
                error: () => {
                    this.notificationService.error('Fail to save db to db');
                },
            });
    }

    public close() {
        this.dialogRef.close();
    }
}
