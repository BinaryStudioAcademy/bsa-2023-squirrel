import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-info-tooltip',
    templateUrl: './info-tooltip.component.html',
    styleUrls: ['./info-tooltip.component.sass'],
    encapsulation: ViewEncapsulation.None,
})
export class InfoTooltipComponent {
    @Input() text: string;
}
