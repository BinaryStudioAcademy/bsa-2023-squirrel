/* eslint-disable no-empty-function */
import { ComponentType } from '@angular/cdk/portal';
import {
    Component,
    ElementRef,
    EventEmitter,
    HostListener,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges,
    TemplateRef,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SafeHtml } from '@angular/platform-browser';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnInit, OnChanges {
    public searchTerm: string = '';

    public isActive = false;

    public faCheckIcon = faCheck;

    @Input() options: string[] = [];

    @Input() width: number;

    @Input() customTemplate: TemplateRef<any> | ComponentType<any>;

    @Input() dropdownIcon: SafeHtml;

    @Output() selectedValueChanged = new EventEmitter<string>();

    @Input() selectedOption: string;

    @HostListener('document:click', ['$event'])
    onClick(event: Event): void {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    constructor(private elementRef: ElementRef, private matDialog: MatDialog) {}

    ngOnInit(): void {
        this.updateSelectedOption();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['options'] && !changes['options'].firstChange) {
            this.updateSelectedOption();
        }
    }

    private updateSelectedOption() {
        if (this.options.length > 0) {
            [this.selectedOption] = this.options;
        }
    }

    onOptionSelected(value: string) {
        if (this.customTemplate && value === this.options.slice(-1)[0]) {
            this.openModal();

            return;
        }

        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    private openModal() {
        this.matDialog.open(this.customTemplate);
    }

    public filterOptions(): string[] {
        const filteredOptions = this.options.filter(
            (option) => option.toLowerCase().includes(this.searchTerm.toLowerCase()),
            // eslint-disable-next-line function-paren-newline
        );

        return filteredOptions;
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
