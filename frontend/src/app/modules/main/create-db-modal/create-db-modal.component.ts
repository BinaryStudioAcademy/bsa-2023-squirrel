import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConsoleConnectService } from '@core/services/console-connect.service';

import { DbConnection } from '../../../models/console/db-connection';
import { DbEngine } from '../../../models/projects/db-engine';

@Component({
    selector: 'app-create-db-modal',
    templateUrl: './create-db-modal.component.html',
    styleUrls: ['./create-db-modal.component.sass'],
})
export class CreateDbModalComponent implements OnInit {
    public dbEngine: DbEngine;

    public dbForm: FormGroup = new FormGroup({});

    constructor(
        @Inject(MAT_DIALOG_DATA) private data: any,
        public dialogRef: MatDialogRef<CreateDbModalComponent>,
        private fb: FormBuilder,
        private consoleConnectService: ConsoleConnectService,
        // eslint-disable-next-line no-empty-function
    ) {
        this.dbEngine = data.dbEngine;
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
        });
    }

    public addDataBase() {
        console.log(this.dbForm.value);

        const connect: DbConnection = {
            serverName: this.dbForm.value.serverName,
            port: this.dbForm.value.port,
            username: this.dbForm.value.username,
            password: this.dbForm.value.password,
            dbEngine: this.dbEngine,
        };

        this.consoleConnectService.connect(connect).subscribe({
            next: guid => {
                console.log(guid);
            },
        });
    }
}
