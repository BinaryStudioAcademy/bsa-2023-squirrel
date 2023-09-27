import { Component, Input } from '@angular/core';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-loading-spinner',
    templateUrl: './loading-spinner.component.html',
})
export class LoadingSpinnerComponent {
    constructor(public spinnerService: SpinnerService) { }

    @Input() public isOverlay: boolean;

    @Input() public size = '20px';

    @Input() public top = '30%';

    @Input() public left = '49%';

    @Input() public position = 'absolute';

    @Input() public margin = '100px auto';
}
