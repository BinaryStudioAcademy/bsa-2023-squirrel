import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-navbar-header',
    templateUrl: './navbar-header.component.html',
    styleUrls: ['./navbar-header.component.sass'],
})
export class NavbarHeaderComponent implements OnInit {
    branches: string[];

    selectedBranch: string;

    ngOnInit(): void {
        this.branches = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];
    }

    onBranchSelected(value: string) {
        this.selectedBranch = value;
    }
}
