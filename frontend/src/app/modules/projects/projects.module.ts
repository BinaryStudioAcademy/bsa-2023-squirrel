import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { ProjectCardComponent } from '@modules/projects/project-card/project-card.component';
import { ProjectsPageComponent } from '@modules/projects/projects-page/projects-page.component';
import { ProjectsRoutingModule } from '@modules/projects/projects-routing.model';
import { SharedModule } from '@shared/shared.module';
import { FormatDatePipe } from './format-date.pipe';

@NgModule({
    declarations: [ProjectsPageComponent, CreateProjectModalComponent, ProjectCardComponent, FormatDatePipe],
    imports: [
        CommonModule,
        SharedModule,
        MatCardModule,
        MatDialogModule,
        ProjectsRoutingModule,
        MatInputModule,
        MatSelectModule,
        FormsModule,
    ],
})
export class ProjectsModule { }
