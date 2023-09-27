import { Component } from '@angular/core';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-authentication-page',
    templateUrl: './authentication-page.component.html',
    styleUrls: ['./authentication-page.component.sass'],
})
export class AuthenticationPageComponent {
    constructor(private spinnerService: SpinnerService) { }
}
