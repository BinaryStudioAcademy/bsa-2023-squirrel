import { Component } from '@angular/core';

@Component({
    selector: 'app-confirmation-dialog',
    template: `
        <h2 mat-dialog-title>Confirmation</h2>
        <mat-dialog-content>You have unsaved changes. Do you really want to leave?</mat-dialog-content>
        <mat-dialog-actions>
            <button mat-button [mat-dialog-close]="true">Yes</button>
            <button mat-button [mat-dialog-close]="false">No</button>
        </mat-dialog-actions>
    `,
})
export class ConfirmationDialogComponent {}
