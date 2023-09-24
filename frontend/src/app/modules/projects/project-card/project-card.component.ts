import { Component, Input, OnInit } from '@angular/core';

import { DbEngine } from '../../../models/projects/db-engine';
import { ProjectResponseDto } from '../../../models/projects/project-response-dto';

@Component({
    selector: 'app-project-card',
    templateUrl: './project-card.component.html',
    styleUrls: ['./project-card.component.sass'],
})
export class ProjectCardComponent implements OnInit {
    @Input() project: ProjectResponseDto;

    public engineLogoImage: string = '';

    private postgresSqlLogo: string = '/assets/postgresql.svg';

    private sqlServerLogo: string = '/assets/sqlserver.svg';

    public DBE = DbEngine;

    ngOnInit(): void {
        this.initializeProjectCard();
    }

    initializeProjectCard(): void {
        this.engineLogoImage =
            this.project.dbEngine === DbEngine.PostgreSQL ? this.postgresSqlLogo : this.sqlServerLogo;
    }
}
