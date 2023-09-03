import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationModalComponent } from '@shared/components/confirmation-modal/confirmation-modal.component';

import { ConfirmationModalInterface } from '../../../../../models/confirmation-modal/confirmation-modal';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent implements OnInit {
    public branches: string[];

    public selectedBranch: string;

    public navLinks: { path: string; displayName: string }[] = [
        { displayName: 'Changes', path: './changes' },
        { displayName: 'PRs', path: './pull-requests' },
        { displayName: 'Branches', path: './branches' },
        { displayName: 'Scripts', path: './scripts' },
        { displayName: 'Code', path: './code' },
        { displayName: 'Settings', path: './settings' },
    ];

    // eslint-disable-next-line no-empty-function
    constructor(private confirmationModal: MatDialog) {
    }

    ngOnInit(): void {
        this.branches = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];
    }

    public onBranchSelected(value: string) {
        this.selectedBranch = value;
    }

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

    performConfirmationModalOne() {
        console.log('The text submitted from the Confirmation Modal ONE');
    }
}
