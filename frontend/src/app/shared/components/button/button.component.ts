import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.sass'],
})
export class ButtonComponent {
    @Input() public text = '';

    @Input() public width = 'auto';

    @Input() public height = '45px';

    @Input() public padding = '10px 20px';

    @Input() public fontSize = '16px';

    @Input() public variant = 'outline-primary';

    @Input() public isDisabled = false;

    @Output() public buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

    public handleClick(): void {
        this.buttonOnClick.emit();
    }
}
