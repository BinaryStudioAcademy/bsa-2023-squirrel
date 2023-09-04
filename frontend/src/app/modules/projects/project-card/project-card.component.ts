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

    public accentColor: string = '';

    public engineLogoImage: string = '';

    public sampleDescription: string =
        '540 saved to favorites lorem ipsum are future lorem is the best thk best thk the';

    public sampleDate: string = '12-12-2024';

    private sqlServerAccentColor: string = '#e79925';

    private postgresSqlAccentColor: string = '#FF6532';

    private postgresSqlLogo: string = '/assets/postgresql.svg';

    private sqlServerLogo: string = '/assets/sqlserver.svg';

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor,no-empty-function,@typescript-eslint/no-empty-function
    constructor() {}

    ngOnInit(): void {
        this.initializeProjectCard();
    }

    initializeProjectCard(): void {
        if (this.project.engine === DbEngine.PostgreSql) {
            this.accentColor = this.postgresSqlAccentColor;
            this.engineLogoImage = this.postgresSqlLogo;
        } else {
            this.accentColor = this.sqlServerAccentColor;
            this.engineLogoImage = this.sqlServerLogo;
        }
    }
}
