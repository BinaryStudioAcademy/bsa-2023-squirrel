import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent implements OnInit {
    @ViewChild('modalContent') modalContent: TemplateRef<any>;

    /*  component for passing as a modal
        import { ComponentType } from '@angular/cdk/portal';
    */
    // modalComponent: ComponentType<NotFoundComponent> = NotFoundComponent;

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

    ngOnInit(): void {
        this.branches = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];
    }

    public onBranchSelected(value: string) {
        this.selectedBranch = value;
    }
}
