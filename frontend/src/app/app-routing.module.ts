import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        data: { requiresToken: false },
        loadChildren: () => import('@modules/authentication/authentication.module').then((m) => m.AuthenticationModule),
    },
    {
        path: 'projects',
        canActivate: [AuthGuard],
        data: { requiresToken: true },
        loadChildren: () => import('./modules/projects/projects.module').then((m) => m.ProjectsModule),
    },
    {
        path: 'projects/:id',
        canActivate: [AuthGuard],
        data: { requiresToken: true },
        loadChildren: () => import('./modules/main/main.module').then((m) => m.MainModule),
    },
    {
        path: 'profile',
        canActivate: [AuthGuard],
        data: { requiresToken: true },
        loadChildren: () => import('./modules/user-profile/user-profile.module').then((m) => m.UserProfileModule),
    },
    {
        path: '**',
        component: NotFoundComponent,
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
