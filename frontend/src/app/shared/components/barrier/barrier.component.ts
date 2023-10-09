import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-barrier',
    templateUrl: './barrier.component.html',
    styleUrls: ['./barrier.component.sass'],
})
export class BarrierComponent {
    @Input() public text?: string;

    @Input() public width?: string;
}
