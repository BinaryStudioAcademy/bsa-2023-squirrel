import {
    Component,
    ElementRef,
    EventEmitter,
    HostListener,
    Input,
    OnInit,
    Output,
    SecurityContext,
    TemplateRef,
    ViewChild,
} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-dropdown-select',
    templateUrl: './dropdown-select.component.html',
    styleUrls: ['./dropdown-select.component.sass'],
})
export class DropdownSelectComponent<T> implements OnInit {
    @Input() public options: T[];

    @Input() public selectedIds: number[] = [];

    @Input() public placeholderId?: number;

    @Input() public height: string = '45px';

    @Input() public width: string = '200px';

    @Input() public placeholder: string = 'Select...';

    @Input() public template: TemplateRef<T>;

    @Input() public chipTemplate: TemplateRef<T>;

    @Input() public filterPredicate?: (item: T, value: string) => boolean;

    @Output() public valueChange = new EventEmitter();

    @ViewChild('inputTrigger') inputElement: ElementRef;

    public input: string;

    private currentId: number = -1;

    public internalOptions: T[];

    public filteredOptions: T[];

    public selectedOptions: T[];

    public currentValue: T;

    public isDropdownOpen: boolean = false;

    public get dropdownElement(): Element {
        return this.element.nativeElement.querySelector('.dropdown-list');
    }

    constructor(private element: ElementRef, private sanitizer: DomSanitizer) {
        // Intentionally left empty for dependency injection purposes only
    }

    public ngOnInit(): void {
        this.internalOptions = [...this.options];
        this.filteredOptions = this.internalOptions;
        this.selectedOptions = [];
        this.selectedIds.forEach((item) => this.selectByIndex(item));
        if (this.placeholderId !== undefined) {
            this.selectByIndex(this.placeholderId);
        }
    }

    public onInputChanged() {
        this.filteredOptions = this.filter(this.input);
        if (!this.isDropdownOpen) {
            this.toggleDropdown();
        }
    }

    // To close dropdown, if user clicked outside of it
    @HostListener('document:click', ['$event'])
    public onClick(event: Event): void {
        if (!this.element.nativeElement.contains(event.target)) {
            this.closeDropdown();
        }
    }

    public focusToInput() {
        this.inputElement.nativeElement.focus();
    }

    private closeDropdown() {
        this.dropdownElement.setAttribute('aria-expanded', 'false');
        this.isDropdownOpen = false;
    }

    public sanitize(value: unknown) {
        return this.sanitizer.sanitize(SecurityContext.HTML, value as string);
    }

    private selectByIndex(i: number) {
        const value = this.internalOptions[i];

        this.select(value);
    }

    public select(value: T) {
        if (this.internalOptions.indexOf(value) === this.placeholderId) {
            this.selectedOptions = [value];
        } else {
            if (this.selectedOptions.some((x) => x === value)) {
                this.selectedOptions = this.selectedOptions.filter((x) => x !== value);
            } else {
                this.selectedOptions = this.selectedOptions.concat(value);
            }
            this.ensurePlaceholderBehavior();
        }
        this.valueChange.emit(this.selectedOptions);
    }

    private ensurePlaceholderBehavior() {
        if (this.placeholderId === undefined) {
            return;
        }
        const placeholder = this.internalOptions[this.placeholderId];

        if (this.selectedOptions.length === 0) {
            this.selectedOptions.push(placeholder);
        } else {
            this.selectedOptions = this.selectedOptions.filter((x) => x !== placeholder);
        }
    }

    public isSelected(option: T) {
        return this.selectedOptions.some((x) => x === option);
    }

    public toggleDropdown() {
        this.isDropdownOpen = !this.isDropdownOpen;
        this.dropdownElement.setAttribute('aria-expanded', this.isDropdownOpen ? 'true' : 'false');
    }

    private filter(filter: string) {
        if (filter) {
            return this.internalOptions.filter(
                this.filterPredicate
                    ? (option) => this.filterPredicate?.call(this, option, filter)
                    : (option) => (option as unknown as string).toLowerCase().indexOf(filter.toLowerCase()) >= 0,
            );
        }

        return this.internalOptions;
    }

    // To allow keyboard manipulations
    @HostListener('document:keydown', ['$event'])
    public handleKeyPressedEvents($event: KeyboardEvent) {
        if (!this.isDropdownOpen) {
            return;
        }
        if (
            $event.key === 'ArrowUp' ||
            $event.key === 'ArrowDown' ||
            $event.key === 'Enter' ||
            $event.key === 'NumpadEnter' ||
            $event.key === 'Escape'
        ) {
            $event.preventDefault();
        }
        if ($event.code === 'ArrowUp') {
            if (this.currentId < 0) {
                this.currentId = 0;
            } else if (this.currentId > 0) {
                this.currentId--;
            }
            this.element.nativeElement.querySelectorAll('li').item(this.currentId).focus();
        } else if ($event.code === 'ArrowDown') {
            if (this.currentId < 0) {
                this.currentId = 0;
            } else if (this.currentId < this.internalOptions.length - 1) {
                this.currentId++;
            }
            this.element.nativeElement.querySelectorAll('li').item(this.currentId).focus();
        } else if (($event.code === 'Enter' || $event.code === 'NumpadEnter') && this.currentId >= 0) {
            this.selectByIndex(this.currentId);
        } else if ($event.code === 'Escape') {
            this.closeDropdown();
        }
    }
}
