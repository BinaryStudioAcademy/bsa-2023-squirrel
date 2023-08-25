import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-project-list',
    templateUrl: './project-list.component.html',
    styleUrls: ['./project-list.component.sass'],
})

export class ProjectListComponent {
    @Input() project: any;

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor,no-empty-function,@typescript-eslint/no-empty-function
    constructor() { }
}
