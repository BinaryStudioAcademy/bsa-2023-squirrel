import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { ProjectsPageComponent } from '@modules/projects/projects-page/projects-page.component';
import {MatCardModule} from "@angular/material/card";

@NgModule({
    declarations: [ProjectsPageComponent],
    imports: [
        CommonModule,
        SharedModule,
        MatCardModule,
    ],
    exports: [
        ProjectsPageComponent
    ]
})
export class ProjectsModule { }
