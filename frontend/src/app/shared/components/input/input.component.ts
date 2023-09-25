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

    public registerOnChange(): void {}

    public registerOnTouched(): void {}

    public writeValue(): void {}

    get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}
