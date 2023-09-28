import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { CanComponentDeactivate } from '@core/guards/unsaved-script.guard';
import { NotificationService } from '@core/services/notification.service';
import { ScriptService } from '@core/services/script.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { faArrowUp } from '@fortawesome/free-solid-svg-icons';
import { finalize, Observable, of, switchMap, takeUntil, tap } from 'rxjs';

import { DatabaseDto } from 'src/app/models/database/database-dto';
import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';
import { ExecuteScriptDto } from 'src/app/models/scripts/execute-script-dto';
import { ScriptContentDto } from 'src/app/models/scripts/script-content-dto';
import { ScriptDto } from 'src/app/models/scripts/script-dto';
import { ScriptErrorDto } from 'src/app/models/scripts/script-error-dto';
import { ScriptResultDto } from 'src/app/models/scripts/script-result-dto';

import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { CreateScriptModalComponent } from '../create-script-modal/create-script-modal.component';

@Component({
    selector: 'app-scripts-page',
    templateUrl: './scripts-page.component.html',
    styleUrls: ['./scripts-page.component.sass'],
})
export class ScriptsPageComponent extends BaseComponent implements OnInit, OnDestroy, CanComponentDeactivate {
    public form: FormGroup;

    public scripts: ScriptDto[] = [];

    public filteredScripts: ScriptDto[] = [];

    public selectedScript: ScriptDto | undefined;

    public scriptErrors: { [scriptId: number]: ScriptErrorDto } = {};

    public scriptResults: { [scriptId: number]: ScriptResultDto } = {};

    public isToTopBtnShowed = false;

    public arrowUp = faArrowUp;

    private project: ProjectResponseDto;

    private currentDb: DatabaseDto;

    private readonly scriptResultComponentSelector = 'app-script-result';

    private readonly scriptErrorComponentSelector = 'app-script-error-result';

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

    public get currentScriptError(): ScriptErrorDto | undefined {
        return this.selectedScript ? this.scriptErrors[this.selectedScript.id] : undefined;
    }

    public get currentScriptResult(): ScriptResultDto | undefined {
        return this.selectedScript ? this.scriptResults[this.selectedScript.id] : undefined;
    }

    public ngOnInit(): void {
        this.loadScripts();
        this.initializeForm();
        this.loadCurrentDb();
        this.registerScroll();
    }

    public override ngOnDestroy() {
        super.ngOnDestroy();
        this.removeScroll();
    }

    public canDeactivate(): Observable<boolean> | boolean {
        if (this.form.get('scriptContent')?.dirty) {
            const dialogRef = this.dialog.open(ConfirmationDialogComponent);

            return dialogRef.afterClosed();
        }

        return true;
    }

    public onScriptSelected(script: ScriptDto): void {
        if (script.id === this.selectedScript?.id) {
            return;
        }

        if (this.form.get('scriptContent')?.dirty) {
            const dialogRef = this.dialog.open(ConfirmationDialogComponent);

            dialogRef.afterClosed().subscribe((confirmed) => {
                if (confirmed) {
                    this.selectScript(script);
                }
            });

            return;
        }

        this.selectScript(script);
    }
  
    public openCreateModal(): void {
        const dialogRef: any = this.dialog.open(CreateScriptModalComponent, {
            width: '450px',
        });

            dialogRef.afterClosed().subscribe((confirmed) => {
                if (confirmed) {
                    this.openCreateModal();
                }
            });
        } else {
            this.openCreateModal();
        }
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
            .pipe(tap({ next: () => this.spinner.hide() }), takeUntil(this.unsubscribe$))
            .subscribe({
                next: (updatedScript: ScriptDto) => {
                    this.selectedScript = updatedScript;
                    this.scripts[this.scripts.findIndex((s) => s.id === updatedScript.id)] = updatedScript;
                    this.form.markAsPristine();
                    this.notification.info('Script is successfully saved');
                },
                error: (err) => this.notification.error(err),
            });
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
            .subscribe({
                next: (updatedContent: ScriptContentDto) => {
                    this.selectedScript!.content = updatedContent.content;
                    this.scripts[this.scripts.findIndex((s) => s.id === this.selectedScript!.id)].content =
                        updatedContent.content;
                    this.updateEditorContent(updatedContent.content);
                    this.form.markAsDirty();
                    this.removeLastErrorForSelectedScript();
                    this.notification.info('Script content successfully formatted');
                },
                error: (err: ScriptErrorDto) => {
                    this.updateScriptContentError(err);
                    this.scrollToResult(false);
                },
            });
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
            .subscribe({
                next: (executedScriptResult: ScriptResultDto) => {
                    this.removeLastErrorForSelectedScript();
                    this.updateScriptResult(executedScriptResult);
                    this.notification.info('Script is successfully executed');
                    this.scrollToResult(true);
                },
                error: (err: ScriptErrorDto) => {
                    this.updateScriptContentError(err);
                    this.scrollToResult(false);
                },
            });
    }

    public scrollToResult(isSuccessful: boolean) {
        setTimeout(() => {
            const targetComponent = document.querySelector(
                isSuccessful ? this.scriptResultComponentSelector : this.scriptErrorComponentSelector,
            );

            if (targetComponent) {
                targetComponent.scrollIntoView({ behavior: 'smooth' });
            }
        }, 0);
    }

    public filterScripts() {
        this.filteredScripts = this.scripts.filter(s =>
            s.fileName.includes(this.form.value.search) || s.id === this.selectedScript?.id);
    }

    public deleteScript(scriptId: number) {
        this.scriptService.deleteScript(scriptId)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe({
                next: () => {
                    this.notification.info('Script successfully deleted');
                    this.scripts.splice(this.scripts.findIndex(s => s.id === scriptId), 1);
                    this.filterScripts();
                    this.selectedScript = undefined;
                    this.form.markAsPristine();
                },
                error: err => {
                    this.notification.error(err.message);
                },
            });
    }

    private onScroll() {
        const element = document.querySelector('app-script-result');
        const elementRect = element?.getBoundingClientRect();

        if (!elementRect) {
            return;
        }
        this.isToTopBtnShowed = elementRect.top < 0;
    }

    private registerScroll() {
        this.parentScroll?.addEventListener('scroll', this.onScroll.bind(this));
    }

    private removeScroll() {
        this.parentScroll?.removeEventListener('scroll', this.onScroll.bind(this));
    }

    private get parentScroll() {
        return document.getElementById('parent-scroll');
    }

    private openCreateModal(): void {
        const dialogRef: any = this.dialog.open(CreateScriptModalComponent, {
            panelClass: 'custom-dialog-container',
            width: '450px',
        });

        dialogRef.componentInstance.scriptCreated.subscribe((newScript: ScriptDto) => {
            this.loadScripts(newScript.id);
            this.selectScript(newScript);
        });
    }

    private removeLastErrorForSelectedScript(): void {
        if (this.selectedScript) {
            delete this.scriptErrors[this.selectedScript.id];
        }
    }

    private updateScriptResult(newResult: ScriptResultDto): void {
        if (this.selectedScript) {
            newResult.date = new Date();
            this.scriptResults[this.selectedScript.id] = newResult;
        }
    }

    private updateScriptContentError(error: ScriptErrorDto): void {
        if (this.selectedScript) {
            error.date = new Date();
            this.scriptErrors[this.selectedScript.id] = error;
        }
    }

    private initializeForm(): void {
        this.form = this.formBuilder.group({
            scriptContent: [this.selectedScript?.content],
            search: [''],
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
            this.filterScripts();
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

    private updateEditorContent(content: string): void {
        this.form.patchValue({
            scriptContent: content,
        });
    }

    private selectScript(script: ScriptDto): void {
        this.selectedScript = script;
        this.updateEditorContent(script.content);
        this.form.markAsPristine();
    }
}
