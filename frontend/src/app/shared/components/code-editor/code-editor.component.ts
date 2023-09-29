import { Component, Input, Self, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
    selector: 'app-code-editor',
    templateUrl: './code-editor.component.html',
    styleUrls: ['./code-editor.component.sass'],
    encapsulation: ViewEncapsulation.None,
})
export class CodeEditorComponent implements ControlValueAccessor {
    @Input() name: string | undefined;

    public codeMirrorOptions: any = {
        mode: 'text/x-mysql',
        indentWithTabs: true,
        smartIndent: true,
        lineNumbers: true,
        lineWrapping: true,
        foldGutter: true,
        extraKeys: { 'Ctrl-Space': 'autocomplete' },
        gutters: ['CodeMirror-linenumbers', 'CodeMirror-foldgutter'],
        autoCloseBrackets: true,
        matchBrackets: true,
        lint: true,
    };

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
