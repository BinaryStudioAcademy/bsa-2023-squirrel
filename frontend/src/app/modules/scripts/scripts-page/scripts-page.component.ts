import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { ScriptService } from '@core/services/script.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { Observable, of, switchMap, takeUntil } from 'rxjs';

import { ScriptDto } from 'src/app/models/scripts/script-dto';

import { CreateScriptModalComponent } from '../create-script-modal/create-script-modal.component';

@Component({
    selector: 'app-scripts-page',
    templateUrl: './scripts-page.component.html',
    styleUrls: ['./scripts-page.component.sass'],
})
export class ScriptsPageComponent extends BaseComponent implements OnInit {
    public scripts: ScriptDto[] = [];

    public selectedScript: ScriptDto | undefined;

    private selectedOptionElement: HTMLLIElement | undefined;

    private readonly selectedOptionClass = 'selected-option';

    constructor(
        public dialog: MatDialog,
        private scriptService: ScriptService,
        private spinner: SpinnerService,
        private sharedProject: SharedProjectService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.loadScripts();
    }

    public onScriptSelected($event: any) {
        const option = $event.option.element as HTMLLIElement;

        if (this.selectedOptionElement) {
            this.selectedOptionElement.classList.remove(this.selectedOptionClass);
        }
        option.classList.add(this.selectedOptionClass);
        this.selectedOptionElement = option;
        [this.selectedScript] = $event.value as ScriptDto[];
    }

    public openCreateModal(): void {
        const dialogRef = this.dialog.open(CreateScriptModalComponent, {
            width: '40%',
            height: '55%',
        });

        dialogRef.componentInstance.scriptCreated.subscribe((newScript: ScriptDto) => {
            this.loadScripts(newScript.id);
        });
    }

    private fetchScripts(): Observable<ScriptDto[]> {
        return this.sharedProject.project$.pipe(
            takeUntil(this.unsubscribe$),
            switchMap((project) => {
                if (project) {
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
