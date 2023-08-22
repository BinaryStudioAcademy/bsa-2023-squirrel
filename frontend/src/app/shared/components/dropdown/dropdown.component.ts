import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.sass'],
})
export class DropdownComponent implements OnInit {
    isActive = false;

    @Input() options: string[] = [];

    @Input() width: number;

    @Output() selectedValueChanged = new EventEmitter<string>();

    selectedOption: string;

    ngOnInit(): void {
        [this.selectedOption] = this.options;
    }

    onOptionSelected(value: string) {
        this.selectedOption = value;
        this.selectedValueChanged.emit(this.selectedOption);
    }

    toggleActiveClass() {
        this.isActive = !this.isActive;
    }
}
