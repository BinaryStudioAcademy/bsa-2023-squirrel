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

    @Input() public options: T[] = [];

    @Input() public width: number;

    @Input() public selectedByDefault: number = 0;

    @Input() public isButtonIncluded: boolean = false;

    @Output() public selectedValueChanged = new EventEmitter();

    @Output() public buttonClicked = new EventEmitter();

    @Input() public dropdownIcon: string;

    @Input() public template: TemplateRef<T>;

    @Input() public modalOption: string = '+ Add New';

    @Input() public filterPredicate?: (item: T, value: string) => boolean = this.filterByName;

    public selectedOption: T;

    @HostListener('document:click', ['$event'])
    public onClick(event: Event): void {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    constructor(
        private elementRef: ElementRef,
    ) {
        // Intentionally left empty for dependency injection purposes only
    }

    public ngOnChanges(changes: SimpleChanges) {
        if (changes['options'] || changes['selectedByDefault']) {
            this.selectedOption = this.options[this.selectedByDefault];
        }
    }

    public onOptionSelected(value: T) {
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
