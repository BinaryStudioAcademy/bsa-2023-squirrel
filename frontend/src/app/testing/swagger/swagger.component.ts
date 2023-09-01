import { Component, OnInit } from '@angular/core';
import { environment } from '@env/environment';

@Component({
    selector: 'app-swagger',
    template: '',
})
export class SwaggerComponent implements OnInit {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor, no-empty-function, @typescript-eslint/no-empty-function
    constructor() {}

    ngOnInit(): void {
        window.location.href = `${environment.coreUrl}/swagger`;
    }
}
