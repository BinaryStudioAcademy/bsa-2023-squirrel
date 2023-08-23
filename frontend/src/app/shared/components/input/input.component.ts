import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html',
    styleUrls: ['./input.component.sass'],
})
export class InputComponent implements ControlValueAccessor {
    @Input() label = '';

    @Input() type = 'text';

    public show = false;

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    public registerOnChange(fn: any): void {
    }

    public registerOnTouched(fn: any): void {
    }

    public writeValue(obj: any): void {
    }

    get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}
