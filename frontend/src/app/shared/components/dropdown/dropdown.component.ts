import { ComponentType } from '@angular/cdk/portal';
import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SafeHtml } from '@angular/platform-browser';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnInit {
    public searchTerm: string = '';

    public isActive = false;

    public faCheckIcon = faCheck;

    @Input() options: string[] = [];

    @Input() width: number;

    @Input() customTemplate: TemplateRef<any> | ComponentType<any>;

    @Input() dropdownIcon: SafeHtml;

    @Output() selectedValueChanged = new EventEmitter<string>();

    public selectedOption: string;

    @HostListener('document:click', ['$event'])
    onClick(event: Event): void {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    // eslint-disable-next-line no-empty-function
    constructor(private elementRef: ElementRef, private matDialog: MatDialog) {}

    ngOnInit(): void {
        [this.selectedOption] = this.options;
    }

    private openModal() {
        this.matDialog.open(this.customTemplate);
    }

    public onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);

        if (this.customTemplate && this.options.indexOf(value) === this.options.length - 1) {
            this.openModal();
        }
    }

    public filterOptions(): string[] {
        const filteredOptions = this.options.filter((option) => {
            return option.toLowerCase().includes(this.searchTerm.toLowerCase());
        });

        return filteredOptions;
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
