import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.sass'],
})
export class ButtonComponent {
    @Input() text = '';

    @Input() width = 'auto';

    @Input() height = 'auto';

    @Input() padding = '10px 20px';

    @Input() fontSize = '16px';

    @Input() variant = 'outline-primary';

    @Input() isDisabled = false;

    @Output() buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

    handleClick(): void {
        this.buttonOnClick.emit();
    }
}
