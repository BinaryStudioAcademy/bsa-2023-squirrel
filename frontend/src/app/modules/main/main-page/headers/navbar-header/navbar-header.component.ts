import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';
import { SvgFileContentFetcher } from '@shared/helpers/svgFileContentFetcher';

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

    private branchesIconPath = 'assets/git-branch.svg';

    public branchesIcon: SafeHtml;

    // eslint-disable-next-line no-empty-function
    constructor(private svgFileContentFetcher: SvgFileContentFetcher) {}

    ngOnInit(): void {
        this.getBranchIcon();
        this.branches = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];
    }

    private getBranchIcon() {
        this.svgFileContentFetcher.fetchSvgContent(this.branchesIconPath).subscribe((response) => {
            if (response !== null) {
                this.branchesIcon = response;
            }
        });
    }

    public onBranchSelected(value: string) {
        this.selectedBranch = value;
    }
}
