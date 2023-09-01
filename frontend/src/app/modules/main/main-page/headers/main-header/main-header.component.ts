import { Component } from '@angular/core';

@Component({
    selector: 'app-main-header',
    templateUrl: './main-header.component.html',
    styleUrls: ['./main-header.component.sass'],
})
export class MainHeaderComponent {
    public projectName: string = 'Project Name';

    public selectedDbName: string;

    public dbNames: string[] = ['Branch 1', 'Branch 2', 'Branch 3', 'Branch 4'];

    public onDatabaseSelected(value: string) {
        this.selectedDbName = value;
    }
}
