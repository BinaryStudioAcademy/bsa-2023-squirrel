import { Component, Input, OnInit } from '@angular/core';

import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-project-card',
    templateUrl: './project-card.component.html',
    styleUrls: ['./project-card.component.sass'],
})
export class ProjectCardComponent implements OnInit {
    @Input() project: ProjectDto;

    public accentColor: string = '';

    public sampleDescription: string =
        '540 saved to favorites lorem ipsum are future lorem is the best thk best thk the';

    public sampleDate: string = '12-12-2024';

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor,no-empty-function,@typescript-eslint/no-empty-function
    constructor() {}

    ngOnInit(): void {
        this.accentColor = this.project.engine === 'PostgreSQL' ? '#FF6532' : '#e79925';
    }
}
