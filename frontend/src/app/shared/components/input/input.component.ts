import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html',
    styleUrls: ['./input.component.sass'],
})
export class InputComponent implements ControlValueAccessor {
    @Input() label = '';

    @Input() name = '';

    @Input() placeholder = 'Write something';

    @Input() type = 'text';

    @Input() width = '100%';

    @Input() height = '45px';

    public show = false;

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    public registerOnChange(): void {}

    public registerOnTouched(): void {}

    public writeValue(): void {}

    public get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}
