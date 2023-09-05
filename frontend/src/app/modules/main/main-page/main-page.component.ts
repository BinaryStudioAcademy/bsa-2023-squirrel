import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BroadcastHubService } from '@core/hubs/broadcast-hub.service';
import { ConsoleLaunchService } from '@core/services/console-launch.service';
import { ConfirmationModalComponent } from '@shared/components/confirmation-modal/confirmation-modal.component';

import { ConfirmationModalInterface } from 'src/app/models/confirmation-modal/confirmation-modal';

@Component({
    selector: 'app-home',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.sass'],
})
export class MainComponent implements OnInit, OnDestroy {
    constructor(
        private broadcastHub: BroadcastHubService,
        public confirmationModal: MatDialog,
        private consoleLaunchService: ConsoleLaunchService,
    ) {
        // do nothing.
    }

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
     * Example method to demonstrate invoking the first Confirmation Modal
     */
    public confirmationModalMessage: string = '';

    openConfirmationModalOne() {
        const modal: ConfirmationModalInterface = {
            modalHeader: 'Reusable Confirmation Modal',
            modalDescription: 'I am first Confirmation Modal to show the example of usage',
            cancelButtonLabel: 'Cancel',
            confirmButtonLabel: 'Submit',
            callbackMethod: () => {
                this.performConfirmationModalOne();
            },
        };

        this.confirmationModal.open(ConfirmationModalComponent, {
            data: modal,
        });
    }

    /**
     * Example method to demonstrate invoking the second Confirmation Modal
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
            data: modal,
        });
    }

    performConfirmationModalOne() {
        this.confirmationModalMessage = 'The text submitted from the Confirmation Modal ONE';
    }

    performConfirmationModalTwo() {
        this.confirmationModalMessage = 'The text submitted from the Confirmation Modal TWO';
    }

    launchConsole() {
        this.consoleLaunchService.launchConsole();
    }
}
