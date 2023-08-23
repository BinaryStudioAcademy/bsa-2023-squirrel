import { Component } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-root',
    template: '<app-loading-spinner></app-loading-spinner><router-outlet></router-outlet>',
})
export class AppComponent {
    constructor(private router: Router, private spinner: SpinnerService) {
        this.listenRouter();
    }

    private listenRouter() {
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationStart) {
                this.spinner.show();
            }
            if (
                event instanceof NavigationEnd ||
                event instanceof NavigationCancel ||
                event instanceof NavigationError
            ) {
                this.spinner.hide();
            }
        });
    }
}
