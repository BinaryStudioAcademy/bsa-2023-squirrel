import { ComponentType } from '@angular/cdk/portal';
import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnInit {
    public searchTerm: string = '';

    public isActive = false;

    @Input() options: any[] = [];

    @Input() width: number;

    @Input() selectedByDefault: number = 0;

    @Output() selectedValueChanged = new EventEmitter<string>();

    @Input() modalTemplate: TemplateRef<any> | ComponentType<any>;

    @Input() dropdownIcon: string;

    @Input() template: TemplateRef<any>;

    @Input() modalOption: string = '+ Add New';

    @Input() filterPredicate?: (item: any, value: string) => boolean = this.filterByName;

    public selectedOption: any;

    @HostListener('document:click', ['$event'])
    onClick(event: Event): void {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    constructor(
        private elementRef: ElementRef,
        private matDialog: MatDialog,
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnInit(): void {
        this.selectedOption = this.options[this.selectedByDefault];
    }

    onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    public openModal() {
        this.matDialog.open(this.modalTemplate);
    }

    public filterOptions(): string[] {
        return this.options.filter((option) => this.filterPredicate?.call(this, option, this.searchTerm));
    }

    public filterByName(option: string, value: string) {
        return option.toLowerCase().includes(value);
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
