import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

import { MainComponent } from './main-page/main-page.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        children: [
            {
                path: 'changes',
                loadChildren: () => import('../changes/changes.module').then((m) => m.ChangesModule),
            },
            {
                path: 'pull-requests',
                loadChildren: () => import('../pull-request/pull-request.module').then((m) => m.PullRequestModule),
            },
            {
                path: 'branches',
                component: NotFoundComponent,
            },
            {
                path: 'scripts',
                loadChildren: () => import('../scripts/scripts.module').then((m) => m.ScriptsModule),
            },
            {
                path: 'code',
                component: NotFoundComponent,
            },
            {
                path: 'settings',
                component: NotFoundComponent,
            },
            {
                path: '**',
                component: NotFoundComponent,
                pathMatch: 'full',
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutingModule {}
