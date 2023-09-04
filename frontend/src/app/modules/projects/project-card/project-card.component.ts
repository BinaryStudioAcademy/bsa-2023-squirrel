import { Component, Input, OnInit } from '@angular/core';

import { DbEngine } from '../../../models/projects/db-engine';
import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-project-card',
    templateUrl: './project-card.component.html',
    styleUrls: ['./project-card.component.sass'],
})
export class ProjectCardComponent implements OnInit {
    @Input() project: ProjectDto;

    public engineLogoImage: string = '';

    private postgresSqlLogo: string = '/assets/postgresql.svg';

    private sqlServerLogo: string = '/assets/sqlserver.svg';

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor,no-empty-function,@typescript-eslint/no-empty-function
    constructor() {}

    ngOnInit(): void {
        this.initializeProjectCard();
    }

    initializeProjectCard(): void {
        this.engineLogoImage = this.project.engine === DbEngine.PostgreSql ? this.postgresSqlLogo : this.sqlServerLogo;
    }
}
