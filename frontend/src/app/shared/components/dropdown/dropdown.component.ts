import { ComponentType } from '@angular/cdk/portal';
import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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

    @Input() options: any[] = [];

    @Input() width: number;

    @Input() modalTemplate: TemplateRef<any> | ComponentType<any>;

    @Input() dropdownIcon: string;

    @Input() template: TemplateRef<any>;

    @Input() modalOption: string = '+ Add New';

    @Input() filterPredicate?: (item: any, value: string) => boolean = this.filterByName;

    @Output() selectedValueChanged = new EventEmitter<any>();

    public selectedOption: any;

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

    onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    public openModal() {
        this.matDialog.open(this.modalTemplate);
    }

    public filterOptions(): string[] {
        const filteredOptions = this.options.filter((option) => this.filterPredicate?.call(this, option, this.searchTerm));

        return filteredOptions;
    }

    public filterByName(option: string, value: string) {
        return option.toLowerCase().includes(value);
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
