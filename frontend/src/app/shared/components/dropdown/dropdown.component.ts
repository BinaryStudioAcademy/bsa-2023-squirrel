import {
    Component,
    ElementRef,
    EventEmitter,
    HostListener,
    Input,
    OnChanges,
    Output,
    SimpleChanges,
    TemplateRef } from '@angular/core';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnChanges {
    public searchTerm: string = '';

    public isActive = false;

    @Input() options: any[] = [];

    @Input() width: number;

    @Input() selectedByDefault: number = 0;

    @Input() includeButton: boolean = false;

    @Output() selectedValueChanged = new EventEmitter<string>();

    @Output() buttonClicked = new EventEmitter();

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
        // eslint-disable-next-line no-empty-function
    ) {}

    ngOnChanges(changes: SimpleChanges) {
        if (changes['options'] || changes['selectedByDefault']) {
            this.selectedOption = this.options[this.selectedByDefault];
            this.selectedValueChanged.emit(this.selectedOption);
        }
    }

    onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    public onButtonClick() {
        this.buttonClicked.emit();
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
