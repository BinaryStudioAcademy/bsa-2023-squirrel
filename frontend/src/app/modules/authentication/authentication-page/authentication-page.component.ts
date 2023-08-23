import { Component } from '@angular/core';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-authentication-page',
    templateUrl: './authentication-page.component.html',
    styleUrls: ['./authentication-page.component.sass'],
})
export class AuthenticationPageComponent {
    // eslint-disable-next-line no-empty-function
    constructor(private spinnerService: SpinnerService) {}
}
