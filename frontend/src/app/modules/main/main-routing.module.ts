import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from '@modules/landing/landing-page/landing-page.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

import { MainComponent } from './main-page/main-page.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        children: [
            {
                path: '',
                component: NotFoundComponent,
                pathMatch: 'full',
            },
            {
                path: 'changes',
                component: NotFoundComponent,
            },
            {
                path: 'pull-requests',
                component: NotFoundComponent,
            },
            {
                path: 'branches',
                component: NotFoundComponent,
            },
            {
                path: 'scripts',
                component: NotFoundComponent,
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
                component: LandingPageComponent,
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
