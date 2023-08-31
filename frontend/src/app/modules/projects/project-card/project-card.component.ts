import { Component, Input } from '@angular/core';

import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-project-card',
    templateUrl: './project-card.component.html',
    styleUrls: ['./project-card.component.sass'],
})

export class ProjectCardComponent {
    @Input() project: ProjectDto;

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor,no-empty-function,@typescript-eslint/no-empty-function
    constructor() { }
}
