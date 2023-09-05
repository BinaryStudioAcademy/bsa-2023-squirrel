import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-create-db-modal',
    templateUrl: './create-db-modal.component.html',
    styleUrls: ['./create-db-modal.component.sass'],
})
export class CreateDbModalComponent {
    // eslint-disable-next-line no-empty-function
    constructor(public dialogRef: MatDialogRef<CreateDbModalComponent>) { }
}
