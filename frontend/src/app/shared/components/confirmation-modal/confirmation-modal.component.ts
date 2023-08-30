import { Component, Inject, OnInit } from '@angular/core';
import { ConfirmationModalInterface } from 'src/app/models/confirmation-modal/confirmation-modal';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.sass']
})
export class ConfirmationModalComponent implements OnInit {

  constructor(
    public confirmationModalRef: MatDialogRef<ConfirmationModalComponent>,
    @Inject(MAT_DIALOG_DATA)
    public confirmationModalData: ConfirmationModalInterface,
    public spinnerService: SpinnerService
  ) { }

  ngOnInit(): void {
  }

  handleConfirmationModalSubmit() {
    this.spinnerService.show();
    setTimeout(() => {
      this.confirmationModalData.callbackMethod();
      this.spinnerService.hide();
    }, 500);
  }
  closeConfirmationModal(): void {
    this.confirmationModalRef.close();
  }
}
