import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { NotificationService } from '@core/services/notification.service';
import { ScriptService } from '@core/services/script.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, Observable, of, switchMap, takeUntil, tap } from 'rxjs';

import { DatabaseDto } from 'src/app/models/database/database-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';
import { ExecuteScriptDto } from 'src/app/models/scripts/execute-script-dto';
import { ScriptContentDto } from 'src/app/models/scripts/script-content-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';
import { ScriptErrorDto } from 'src/app/models/scripts/script-error-dto';
import { ScriptResultDto } from 'src/app/models/scripts/script-result-dto';

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

    public scriptErrors: { [scriptId: number]: ScriptErrorDto } = {};

    public scriptResults: { [scriptId: number]: ScriptResultDto } = {};

    get currentScriptError(): ScriptErrorDto | undefined {
        return this.selectedScript ? this.scriptErrors[this.selectedScript.id] : undefined;
    }

    get currentScriptResult(): ScriptResultDto | undefined {
        return this.selectedScript ? this.scriptResults[this.selectedScript.id] : undefined;
    }

    private selectedOptionElement: HTMLLIElement | undefined;

    private readonly selectedOptionClass = 'selected-option';

    private project: ProjectResponseDto;

    private currentDb: DatabaseDto;

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
        this.loadCurrentDb();
    }

    public onScriptSelected($event: any): void {
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
        const script: ExecuteScriptDto = {
            projectId: this.selectedScript.projectId,
            content: this.form.value.scriptContent,
            dbEngine: this.project.dbEngine,
            clientId: null,
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
                    // Clear the error for the current script
                    if (this.selectedScript) {
                        delete this.scriptErrors[this.selectedScript.id];
                    }
                    this.notification.info('Script content successfully formatted');
                },
                (err: ScriptErrorDto) => {
                    this.notification.error('Format Script error');
                    this.updateScriptContentError(err);
                },
            );
    }

    public executeScript(): void {
        if (!this.selectedScript) {
            return;
        }

        this.spinner.show();
        const script: ExecuteScriptDto = {
            projectId: this.selectedScript.projectId,
            content: this.form.value.scriptContent,
            dbEngine: this.project.dbEngine,
            clientId: this.currentDb.guid,
        };

        this.scriptService
            .executeScript(script)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe(
                (executed: ScriptResultDto) => {
                    // Clear the error for the current script
                    if (this.selectedScript) {
                        delete this.scriptErrors[this.selectedScript.id];
                    }
                    this.updateScriptResult(executed);
                    this.notification.info('Script successfully executed');
                    this.scrollToResult('.script-result');
                },
                (err: ScriptErrorDto) => {
                    this.notification.error('Script execution error');
                    this.updateScriptContentError(err);
                    this.scrollToResult('.script-error');
                },
            );
    }

    public updateScriptContent(newContent: string): void {
        if (this.selectedScript) {
            this.selectedScript.content = newContent;
            this.form.patchValue({
                scriptContent: this.selectedScript.content,
            });
            this.form.markAsDirty();
        }
    }

    public updateScriptResult(newResult: ScriptResultDto): void {
        if (this.selectedScript) {
            newResult.date = new Date();
            this.scriptResults[this.selectedScript.id] = newResult;
        }
    }

    public updateScriptContentError(error: ScriptErrorDto): void {
        if (this.selectedScript) {
            error.date = new Date();
            this.scriptErrors[this.selectedScript.id] = error;
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

    private loadCurrentDb() {
        let hasReceivedData = false;

        this.sharedProject.currentDb$.pipe(takeUntil(this.unsubscribe$)).subscribe({
            next: (currentDb) => {
                if (!currentDb) {
                    if (hasReceivedData) {
                        this.notification.error('No database currently selected!');
                    }
                } else {
                    this.currentDb = currentDb;
                    this.notification.info(`Current database client id is: '${this.currentDb.guid}'`);
                }
                hasReceivedData = true;
            },
        });
    }

    private scrollToResult(resultDivSector: string) {
        const targetDiv = document.querySelector(resultDivSector);

        if (targetDiv) {
            targetDiv.scrollIntoView({ behavior: 'smooth' });
        }
    }
}
