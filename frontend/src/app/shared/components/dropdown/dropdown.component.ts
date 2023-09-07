import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnInit {
    public isActive = false;

    @Input() options: string[] = [];

    @Input() width: number;

    @Input() selectedByDefault: number = 0;

    @Output() selectedValueChanged = new EventEmitter<string>();

    public selectedOption: string;

    @HostListener('document:click', ['$event'])
    onClick(event: Event): void {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    // eslint-disable-next-line no-empty-function
    constructor(private elementRef: ElementRef) {}

    ngOnInit(): void {
        this.selectedOption = this.options[this.selectedByDefault];
    }

    public onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    public toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
