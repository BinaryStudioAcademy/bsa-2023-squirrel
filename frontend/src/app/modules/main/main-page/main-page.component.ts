import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BroadcastHubService } from '@core/hubs/broadcast-hub.service';
import { ConfirmationModalComponent } from '@shared/components/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalInterface } from 'src/app/models/confirmation-modal/confirmation-modal';

@Component({
    selector: 'app-home',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.sass'],
})
export class MainComponent implements OnInit, OnDestroy {
    // eslint-disable-next-line no-empty-function
    constructor(private broadcastHub: BroadcastHubService, public confirmationModal: MatDialog) { }

    async ngOnInit() {
        await this.broadcastHub.start();
        this.broadcastHub.listenMessages((msg) => {
            console.info(`The next broadcast message was received: ${msg}`);
        });
    }

    ngOnDestroy() {
        this.broadcastHub.stop();
    }

    /**
     * This method invokes the first Confirmation Modal
     */
    public confirmationModalMessage: string = '';
    openConfirmationModalOne() {
        const modal: ConfirmationModalInterface = {
            modalHeader: 'Created by reusable Confirmation Modal',
            modalDescription: 'I am first Confirmation Modal',
            cancelButtonLabel: 'Cancel',
            confirmButtonLabel: 'Submit',
            callbackMethod: () => {
                this.performConfirmationModalOne();
            },
        };
        this.confirmationModal.open(ConfirmationModalComponent, {
            width: '400px',
            data: modal,
        });
    }

    /**
     * This method invokes the second Confirmation Modal
     */
    openConfirmationModalTwo() {
        const modal: ConfirmationModalInterface = {
            modalHeader: 'Created by reusable Confirmation Modal',
            modalDescription: 'I am second Confirmation Modal',
            cancelButtonLabel: 'Cancel',
            confirmButtonLabel: 'Submit',
            callbackMethod: () => {
                this.performConfirmationModalTwo();
            },
        };
        this.confirmationModal.open(ConfirmationModalComponent, {
            width: '400px',
            data: modal,
        });
    }

    performConfirmationModalOne() {
        this.confirmationModalMessage = 'The dialog submitted from the Dialog ONE';
    }

    performConfirmationModalTwo() {
        this.confirmationModalMessage = 'The dialog submitted from the Dialog TWO';
    }
}
