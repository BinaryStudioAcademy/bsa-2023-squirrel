import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ProjectService } from '@core/services/project.service';

import { EngineEnum } from '../../../models/engine-enum';
import { ProjectDto } from '../../../models/project-dto';

@Component({
    selector: 'app-create-project-modal',
    templateUrl: './create-project-modal.component.html',
    styleUrls: ['./create-project-modal.component.sass'],
})
export class CreateProjectModalComponent {
    projectName: string = '';

    selectedEngine: EngineEnum = EngineEnum.SqlServer;

    constructor(
        private projectService: ProjectService,
        public dialogRef: MatDialogRef<CreateProjectModalComponent>,
        // eslint-disable-next-line no-empty-function
    ) {}

    createProject(): void {
        if (this.projectName && this.selectedEngine) {
            const newProject: ProjectDto = {
                name: this.projectName,
                engine: this.selectedEngine,
            };

            this.projectService.addProject(newProject).subscribe(
                (createdProject: ProjectDto) => {
                    this.dialogRef.close(createdProject);
                },
                (error: any) => {
                    console.error('Error creating project:', error);
                },
            );
        }
    }

    close(): void {
        this.dialogRef.close();
    }
}
