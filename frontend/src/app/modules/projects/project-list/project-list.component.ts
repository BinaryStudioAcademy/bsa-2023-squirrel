import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-project-list',
    templateUrl: './project-list.component.html',
    styleUrls: ['./project-list.component.sass'],
})

export class ProjectListComponent implements OnInit {
    @Input() project: any;

    constructor() { }

    ngOnInit(): void {
    }
}
