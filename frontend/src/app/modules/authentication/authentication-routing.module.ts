import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationPageComponent } from '@modules/authentication/authentication-page/authentication-page.component';
import { LoginComponent } from '@modules/authentication/login/login.component';
import { RegistrationComponent } from '@modules/authentication/registration/registration.component';

const routes: Routes = [
    {
        path: '',
        component: AuthenticationPageComponent,
        children: [
            { path: 'registration', component: RegistrationComponent },
            { path: 'login', component: LoginComponent },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AuthenticationRoutingModule {}
