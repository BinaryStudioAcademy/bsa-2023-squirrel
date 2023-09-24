import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-checkbox',
    templateUrl: './checkbox.component.html',
    styleUrls: ['./checkbox.component.sass'],
})
export class CheckboxComponent {
    @Input() label: string = '';

    @Input() isChecked: boolean | undefined = false;

    @Output() checkedChange: EventEmitter<boolean> = new EventEmitter<boolean>();

    public onCheckboxChange(): void {
        this.isChecked = !this.isChecked;
        this.checkedChange.emit(this.isChecked);
    }
}
