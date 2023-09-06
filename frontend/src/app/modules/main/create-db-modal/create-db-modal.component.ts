import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-create-db-modal',
    templateUrl: './create-db-modal.component.html',
    styleUrls: ['./create-db-modal.component.sass'],
})
export class CreateDbModalComponent implements OnInit {
    public dbForm: FormGroup = new FormGroup({});

    // eslint-disable-next-line no-empty-function
    constructor(public dialogRef: MatDialogRef<CreateDbModalComponent>, private fb: FormBuilder) { }

    public ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.dbForm = this.fb.group({
            dbName: [''],
        });
    }
}
