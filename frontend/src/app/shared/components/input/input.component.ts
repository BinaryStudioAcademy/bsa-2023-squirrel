import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html',
    styleUrls: ['./input.component.sass'],
})
export class InputComponent implements ControlValueAccessor {
    @Input() public label = '';

    @Input() public name = '';

    @Input() public placeholder = 'Write something';

    @Input() public type = 'text';

    @Input() public width = '100%';

    @Input() public height = '45px';

    public show = false;

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    public registerOnChange(): void {
        // Implement this to register a callback function when the input value changes
    }

    public registerOnTouched(): void {
        // Implement this to register a callback function when the input is touched.
    }

    public writeValue(): void {
        // Implement this to write a new value to the input
    }

    public get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}
