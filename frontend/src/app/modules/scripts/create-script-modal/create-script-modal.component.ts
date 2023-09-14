import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { ScriptService } from '@core/services/script.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { takeUntil } from 'rxjs';

import { CreateScriptDto } from 'src/app/models/scripts/create-script-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';

@Component({
    selector: 'app-create-script-modal',
    templateUrl: './create-script-modal.component.html',
    styleUrls: ['./create-script-modal.component.sass'],
})
export class CreateScriptModalComponent extends BaseComponent implements OnInit {
    @Output() public scriptCreated = new EventEmitter<ScriptDto>();

    public newScriptForm: FormGroup;

    constructor(
        public dialogRef: MatDialogRef<CreateScriptModalComponent>,
        private formBuilder: FormBuilder,
        private spinner: SpinnerService,
        private scriptService: ScriptService,
        private sharedProject: SharedProjectService,
    ) {
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

    public createScript(): void {
        this.spinner.show();

        this.sharedProject.project$.pipe(takeUntil(this.unsubscribe$)).subscribe((project) => {
            if (project) {
                const newScriptDto: CreateScriptDto = {
                    title: this.newScriptForm.value.scriptName,
                    fileName: this.newScriptForm.value.fileName,
                    projectId: project.id as number,
                };

                this.scriptService
                    .createScript(newScriptDto)
                    .pipe(takeUntil(this.unsubscribe$))
                    .subscribe((createdScript: ScriptDto) => {
                        this.scriptCreated.emit(createdScript);
                        this.close();
                    });
            }
            this.spinner.hide();
        });
    }

    public close(): void {
        this.dialogRef.close();
    }
}
