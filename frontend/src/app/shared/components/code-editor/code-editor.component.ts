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
    }

    public registerOnTouched(): void {
    }

    public writeValue(): void {
    }

    public get control(): FormControl {
        return this.ngControl.control as FormControl;
    }
}
