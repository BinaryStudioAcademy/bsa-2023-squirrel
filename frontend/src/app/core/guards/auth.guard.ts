import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {
    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService, private router: Router) { }

    public canActivate(route: ActivatedRouteSnapshot) {
        return this.checkForActivation(route);
    }

    public canActivateChild(childRoute: ActivatedRouteSnapshot) {
        return this.checkForActivation(childRoute);
    }

    private checkForActivation(route: ActivatedRouteSnapshot) {
        const requiresToken = route.data['requiresToken'] as boolean;
        const token = this.authService.tokenExist();

        if ((requiresToken && token) || (!requiresToken && !token)) {
            return true;
        }
        this.router.navigateByUrl(requiresToken ? '/login' : '/projects');

        return false;
    }
}
