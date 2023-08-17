import { Component } from '@angular/core';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-landing-page',
    templateUrl: './landing-page.component.html',
    styleUrls: ['./landing-page.component.sass'],
})
export class LandingPageComponent {
    // eslint-disable-next-line no-empty-function
    constructor(private spinnerService: SpinnerService) {}
}
