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
        public confirmationModalRef: MatDialogRef<ConfirmationModalComponent>,
        @Inject(MAT_DIALOG_DATA)
        public confirmationModalData: ConfirmationModalInterface,
        public spinnerService: SpinnerService,
    ) {
        // do nothing.
    }

    handleConfirmationModalSubmit() {
        this.spinnerService.show();
        setTimeout(() => {
            this.confirmationModalData.callbackMethod();
            this.spinnerService.hide();
            this.confirmationModalRef.close();
        }, 500);
    }

    closeConfirmationModal(): void {
        this.confirmationModalRef.close();
    }
}