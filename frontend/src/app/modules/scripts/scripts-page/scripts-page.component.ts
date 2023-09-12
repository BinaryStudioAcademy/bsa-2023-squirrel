import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';

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

    constructor(public dialog: MatDialog) {
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
            width: '500px',
            height: '45%',
        });

        dialogRef.componentInstance.scriptCreated.subscribe((newScript: ScriptDto) => {
            // TODO: add newScript to db via service.

            this.loadScripts();
            // temporarily
            this.scripts.push(newScript);
            this.selectedScript = this.scripts.find((s) => s.id === newScript.id);
        });
    }

    private loadScripts(): void {
        // TODO: load all scripts for current project from web api.
        this.scripts = [
            {
                id: 1,
                title: 'first',
                content: 'firstcontent',
                fileName: '',
                projectId: 1,
            },
            {
                id: 2,
                title: 'second',
                content: 'secondcontent',
                fileName: '',
                projectId: 1,
            },
            {
                id: 3,
                title: 'third',
                content: 'thirdcontent',
                fileName: '',
                projectId: 1,
            },
            {
                id: 4,
                title: 'fourth',
                content: 'fourthcontent',
                fileName: '',
                projectId: 1,
            },
            {
                id: 5,
                title: 'fifth234',
                content: 'fifthcontent',
                fileName: '',
                projectId: 1,
            },
        ];
    }
}
