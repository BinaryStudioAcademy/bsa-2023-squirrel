import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsPageComponent } from '@modules/projects/projects-page/projects-page.component';

const routes: Routes = [
    {
        path: '',
        component: ProjectsPageComponent,
        children: [
            { path: 'projects', component: ProjectsPageComponent },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ProjectsRoutingModule {}
