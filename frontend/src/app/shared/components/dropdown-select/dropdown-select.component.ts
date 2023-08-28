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
export class DropdownSelectComponent implements OnInit {
    @Input() options: any[];

    @Input() selectedIds: number[];

    @Input() height: string = '45px';

    @Input() width: string = '200px';

    @Input() placeholder: string = 'Select...';

    @Input() template: TemplateRef<any>;

    @Input() chipTemplate: TemplateRef<any>;

    @Input() filterPredicate?: (item: any, value: string) => boolean;

    @Output() valueChange = new EventEmitter();

    @ViewChild('inputTrigger') inputElement: ElementRef;

    public input: string;

    private currentId: number = -1;

    public internalOptions: any[];

    public filteredOptions: any[];

    public selectedOptions: any[];

    public currentValue: any;

    public dropdownOpen: boolean = false;

    public get dropdownElement(): Element {
        return this.element.nativeElement.querySelector('.dropdown-list');
    }

    // eslint-disable-next-line no-empty-function
    constructor(private element: ElementRef, private sanitizer: DomSanitizer) {}

    ngOnInit(): void {
        this.internalOptions = [...this.options];
        this.filteredOptions = this.internalOptions;
        this.selectedOptions = [];
        this.selectedIds.forEach((item) => this.selectByIndex(item));
    }

    onInputChanged() {
        this.filteredOptions = this.filter(this.input);
        if (!this.dropdownOpen) {
            this.toggleDropdown();
        }
    }

    // To close dropdown, if user clicked outside of it
    @HostListener('document:click', ['$event'])
    onClick(event: Event): void {
        if (!this.element.nativeElement.contains(event.target)) {
            this.closeDropdown();
        }
    }

    focusToInput() {
        this.inputElement.nativeElement.focus();
    }

    closeDropdown() {
        this.dropdownElement.setAttribute('aria-expanded', 'false');
        this.dropdownOpen = false;
    }

    sanitize(value: any) {
        return this.sanitizer.sanitize(SecurityContext.HTML, value);
    }

    selectByIndex(i: number) {
        const value = this.internalOptions[i];

        this.select(value);
    }

    select(value: any) {
        if (this.selectedOptions.some((x) => x === value)) {
            this.selectedOptions = this.selectedOptions.filter((x) => x !== value);
        } else {
            this.selectedOptions = this.selectedOptions.concat(value);
        }
        this.valueChange.emit(this.selectedOptions);
    }

    isSelected(option: any) {
        return this.selectedOptions.some((x) => x === option);
    }

    toggleDropdown() {
        this.dropdownOpen = !this.dropdownOpen;
        this.dropdownElement.setAttribute('aria-expanded', this.dropdownOpen ? 'true' : 'false');
    }

    filter(filter: string) {
        if (filter) {
            return this.internalOptions.filter(
                this.filterPredicate
                    ? (option) => this.filterPredicate?.call(this, option, filter)
                    : (option) => option.toLowerCase().indexOf(filter.toLowerCase()) >= 0,
            );
        }

        return this.internalOptions;
    }

    // To allow keyboard manipulations
    @HostListener('document:keydown', ['$event'])
    handleKeyPressedEvents($event: KeyboardEvent) {
        if (!this.dropdownOpen) {
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
