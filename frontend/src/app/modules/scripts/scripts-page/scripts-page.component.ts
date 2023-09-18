import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ScriptService } from '@core/services/script.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, Observable, of, switchMap, takeUntil, tap } from 'rxjs';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';
import { RunScriptDto } from 'src/app/models/scripts/run-script-dto';
import { ScriptContentDto } from 'src/app/models/scripts/script-content-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';

import { CreateScriptModalComponent } from '../create-script-modal/create-script-modal.component';

@Component({
    selector: 'app-scripts-page',
    templateUrl: './scripts-page.component.html',
    styleUrls: ['./scripts-page.component.sass'],
})
export class ScriptsPageComponent extends BaseComponent implements OnInit {
    public form: FormGroup;

    public scripts: ScriptDto[] = [];

    public selectedScript: ScriptDto | undefined;

    private selectedOptionElement: HTMLLIElement | undefined;

    private readonly selectedOptionClass = 'selected-option';

    private project: ProjectResponseDto;

    constructor(
        public dialog: MatDialog,
        private formBuilder: FormBuilder,
        private scriptService: ScriptService,
        private spinner: SpinnerService,
        private sharedProject: SharedProjectService,
        private notification: NotificationService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.loadScripts();
        this.initializeForm();
    }

    public onScriptSelected($event: any) {
        const option = $event.option.element as HTMLLIElement;

        if (this.selectedOptionElement) {
            this.selectedOptionElement.classList.remove(this.selectedOptionClass);
        }
        option.classList.add(this.selectedOptionClass);
        this.selectedOptionElement = option;
        [this.selectedScript] = $event.value as ScriptDto[];
        this.form.patchValue({
            scriptContent: this.selectedScript ? this.selectedScript.content : '',
        });
    }

    public openCreateModal(): void {
        const dialogRef: any = this.dialog.open(CreateScriptModalComponent, {
            width: '450px',
            height: '400px',
        });

        dialogRef.componentInstance.scriptCreated.subscribe((newScript: ScriptDto) => {
            this.loadScripts(newScript.id);
        });
    }

    public saveScript(): void {
        if (!this.selectedScript) {
            return;
        }

        this.spinner.show();
        const script: ScriptDto = {
            id: this.selectedScript.id,
            title: this.selectedScript.title,
            fileName: this.selectedScript.fileName,
            content: this.form.value.scriptContent,
            projectId: this.selectedScript.projectId,
        };

        this.scriptService
            .updateScript(script)
            .pipe(tap(() => this.spinner.hide(), takeUntil(this.unsubscribe$)))
            .subscribe(
                (updatedScript: ScriptDto) => {
                    if (this.selectedScript && this.selectedScript.id === updatedScript.id) {
                        this.updateScriptContent(updatedScript.content);
                    }
                    this.form.markAsPristine();
                    this.notification.info('Script is successfully saved');
                },
                (err) => this.notification.error(err),
            );
    }

    public formatScript(): void {
        if (!this.selectedScript) {
            return;
        }

        this.spinner.show();
        const script: RunScriptDto = {
            projectId: this.selectedScript.projectId,
            content: this.form.value.scriptContent,
            dbEngine: this.project.dbEngine,
        };

        this.scriptService
            .formatScript(script)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe(
                (updatedScript: ScriptContentDto) => {
                    this.updateScriptContent(updatedScript.content);
                    this.notification.info('Script content successfully formatted');
                },
                (err) => this.notification.error(JSON.stringify(err)),
            );
    }

    public updateScriptContent(newContent: string): void {
        if (this.selectedScript) {
            this.selectedScript.content = newContent;
            this.form.patchValue(
                {
                    scriptContent: newContent,
                },
                { emitEvent: false },
            );
        }
    }

    private initializeForm(): void {
        this.form = this.formBuilder.group({
            scriptContent: [this.selectedScript?.content],
        });
    }

    private fetchScripts(): Observable<ScriptDto[]> {
        return this.sharedProject.project$.pipe(
            takeUntil(this.unsubscribe$),
            switchMap((project) => {
                if (project) {
                    this.project = project;

                    return this.scriptService.getAllScripts(project.id).pipe(takeUntil(this.unsubscribe$));
                }

                return of([]);
            }),
        );
    }

    private loadScripts(selectedScriptId: number | undefined = undefined): void {
        this.spinner.show();
        this.fetchScripts().subscribe((scripts) => {
            this.scripts = scripts.sort((a, b) => b.id - a.id);
            if (selectedScriptId) {
                this.selectedScript = this.scripts.find((s) => s.id === selectedScriptId);
            }
            this.spinner.hide();
        });
    }
}
