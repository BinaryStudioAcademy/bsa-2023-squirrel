import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { UserProfileComponent } from '@modules/user-profile/user-profile.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        data: { requiresToken: false },
        loadChildren: () => import('@modules/authentication/authentication.module').then((m) => m.AuthenticationModule),
    },

    {
        path: 'main',
        canActivate: [AuthGuard],
        data: { requiresToken: true },
        loadChildren: () => import('./modules/main/main.module').then((m) => m.MainModule),
    },
    {
        path: 'profile',
        data: { requiresToken: true },
        component: UserProfileComponent,
        canActivate: [AuthGuard],
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
