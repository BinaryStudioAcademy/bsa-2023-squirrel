import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';

import { ScriptDto } from 'src/app/models/scripts/script-dto';

@Component({
    selector: 'app-create-script-modal',
    templateUrl: './create-script-modal.component.html',
    styleUrls: ['./create-script-modal.component.sass'],
})
export class CreateScriptModalComponent extends BaseComponent implements OnInit {
    @Output() public scriptCreated = new EventEmitter<ScriptDto>();

    public newScriptForm: FormGroup;

    constructor(public dialogRef: MatDialogRef<CreateScriptModalComponent>, private formBuilder: FormBuilder) {
        super();
    }

    ngOnInit(): void {
        this.initForm();
    }

    public initForm(): void {
        this.newScriptForm = this.formBuilder.group({
            scriptName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
            fileName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
        });
    }

    public close(): void {
        this.dialogRef.close();
    }
}
