import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent implements OnInit {
    projectName: string;

    selectedDbName: string;

    dbNames: string[] = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];

    ngOnInit(): void {
        this.projectName = 'Project Name';
    }

    onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }
}
