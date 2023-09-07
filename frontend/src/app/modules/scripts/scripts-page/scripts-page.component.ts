import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '@core/base/base.component';

import { ScriptDto } from 'src/app/models/scripts/script-dto';

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

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor() {
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

    private loadScripts(): void {
        // TODO: load all scripts for current project from web api.
        this.scripts = [
            {
                id: 1,
                title: 'first',
                content: 'firstcontent',
            },
            {
                id: 2,
                title: 'second',
                content: 'secondcontent',
            },
            {
                id: 3,
                title: 'third',
                content: 'thirdcontent',
            },
            {
                id: 4,
                title: 'fourth',
                content: 'fourthcontent',
            },
            {
                id: 5,
                title: 'fifth234',
                content: 'fifthcontent',
            },
        ];
    }
}
