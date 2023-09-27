import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SpinnerService } from '@core/services/spinner.service';

import { ConfirmationModalInterface } from 'src/app/models/confirmation-modal/confirmation-modal';

@Component({
    selector: 'app-confirmation-modal',
    templateUrl: './confirmation-modal.component.html',
    styleUrls: ['./confirmation-modal.component.sass'],
})
export class ConfirmationModalComponent {
    constructor(
        @Inject(MAT_DIALOG_DATA) public confirmationModalData: ConfirmationModalInterface,
        public confirmationModalRef: MatDialogRef<ConfirmationModalComponent>,
        public spinnerService: SpinnerService,
    ) { }

    public handleConfirmationModalSubmit() {
        this.spinnerService.show();
        this.confirmationModalData.callbackMethod();
        this.spinnerService.hide();
        this.confirmationModalRef.close();
    }

    public closeConfirmationModal(): void {
        this.confirmationModalRef.close();
    }
}
