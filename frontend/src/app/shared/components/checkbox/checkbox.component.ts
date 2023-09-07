import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-checkbox',
    templateUrl: './checkbox.component.html',
    styleUrls: ['./checkbox.component.sass'],
})
export class CheckboxComponent {
    @Input() label: string = '';

    @Input() checked: boolean | undefined = false;

    @Output() checkedChange: EventEmitter<boolean> = new EventEmitter<boolean>();

    public onCheckboxChange(): void {
        this.checked = !this.checked;
        this.checkedChange.emit(this.checked);
    }
}
