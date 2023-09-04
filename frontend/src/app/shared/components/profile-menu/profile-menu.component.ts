import { Component } from '@angular/core';
import { AuthService } from '@core/services/auth.service';

@Component({
    selector: 'app-profile-menu',
    templateUrl: './profile-menu.component.html',
    styleUrls: ['./profile-menu.component.sass'],
})
export class ProfileMenuComponent {
    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService) {}

    signOut() {
        this.authService.signOut();
    }
}
