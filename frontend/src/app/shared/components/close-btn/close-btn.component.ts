import { Component, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-close-btn',
    templateUrl: './close-btn.component.html',
    styleUrls: ['./close-btn.component.sass'],
})
export class CloseBtnComponent {
    @Output() buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

    public click(): void {
        this.buttonOnClick.emit();
    }
}
