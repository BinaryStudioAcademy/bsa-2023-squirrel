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
export class DropdownComponent<T> implements OnChanges {
    public searchTerm: string = '';

    public isActive = false;

    @Input() options: T[] = [];

    @Input() width: number;

    @Input() selectedByDefault: number = 0;

    @Input() isButtonIncluded: boolean = false;

    @Output() selectedValueChanged = new EventEmitter();

    @Output() buttonClicked = new EventEmitter();

    @Input() dropdownIcon: string;

    @Input() template: TemplateRef<T>;

    @Input() modalOption: string = '+ Add New';

    @Input() filterPredicate?: (item: T, value: string) => boolean = this.filterByName;

    public selectedOption: T;

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
        }
    }

    onOptionSelected(value: T) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    public onButtonClick() {
        this.buttonClicked.emit();
    }

    public filterOptions(): T[] {
        return this.options.filter((option) => this.filterPredicate?.call(this, option, this.searchTerm)) as T[];
    }

    public filterByName(option: T, value: string) {
        return (option as unknown as string).toLowerCase().includes(value);
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
