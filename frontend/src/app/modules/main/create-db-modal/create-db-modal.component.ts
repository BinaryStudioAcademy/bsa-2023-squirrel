import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConsoleConnectService } from '@core/services/console-connect.service';
import { DatabaseService } from '@core/services/database.service';
import { NotificationService } from '@core/services/notification.service';

import { DbConnection } from '../../../models/console/db-connection';
import { NewDatabaseDto } from '../../../models/database/new-database-dto';

@Component({
    selector: 'app-create-db-modal',
    templateUrl: './create-db-modal.component.html',
    styleUrls: ['./create-db-modal.component.sass'],
})
export class CreateDbModalComponent implements OnInit {
    @Output() public dbName = new EventEmitter<string>();

    public dbForm: FormGroup = new FormGroup({});

    public localhost: boolean = false;

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        private fb: FormBuilder,
        private consoleConnectService: ConsoleConnectService,
        private databaseService: DatabaseService,
        private notificationService: NotificationService,
        public dialogRef: MatDialogRef<CreateDbModalComponent>,
        // eslint-disable-next-line no-empty-function
    ) {}

    public ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.dbForm = this.fb.group({
            dbName: ['', Validators.required],
            serverName: ['', this.getServerNameValidators()],
            port: [''],
            username: [''],
            password: [''],
        });
    }

    public addDataBase() {
        console.log(this.dbForm.value);

        const connect: DbConnection = {
            dbName: this.dbForm.value.dbName,
            serverName: this.dbForm.value.serverName,
            port: +this.dbForm.value.port,
            username: this.dbForm.value.username,
            password: this.dbForm.value.password,
            dbEngine: this.data.dbEngine - 1,
            isLocalhost: this.localhost,
        };

        this.consoleConnectService.connect(connect).subscribe({
            next: guid => {
                this.saveDb(guid);
            },
            error: () => {
                this.notificationService.error('Failed to connect to database');
            },
        });
    }

    public changeLocalHost() {
        this.localhost = !this.localhost;

        this.dbForm.get('serverName')?.setValidators(this.getServerNameValidators());
        this.dbForm.get('serverName')?.updateValueAndValidity();
    }

    private getServerNameValidators() {
        if (this.localhost) {
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
                next: dbName => {
                    this.notificationService.info('database was successfully added');
                    this.dbName.emit(dbName.dbName);
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
