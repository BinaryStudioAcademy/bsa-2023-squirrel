import { Component, Input } from '@angular/core';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent {
    @Input() project: ProjectResponseDto;

    public selectedDbName: string;

    public dbNames: string[] = ['Dev DB', 'DB 2', 'Db 3', 'Db 4'];

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }
}
