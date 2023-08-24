import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { CreateProjectModalComponent } from '@modules/projects/create-project-modal/create-project-modal.component';
import { ProjectsPageComponent } from '@modules/projects/projects-page/projects-page.component';
import { SharedModule } from '@shared/shared.module';
import { ProjectsRoutingModule } from '@modules/projects/projects-routing.model';
import { MatDialogModule } from '@angular/material/dialog';
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";

@NgModule({
    declarations: [ProjectsPageComponent, CreateProjectModalComponent],
    imports: [
        CommonModule,
        SharedModule,
        MatCardModule,
        MatDialogModule,
        ProjectsRoutingModule,
        MatInputModule,
        MatSelectModule,
    ],
})
export class ProjectsModule { }
