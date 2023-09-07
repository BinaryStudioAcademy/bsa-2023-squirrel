import { Component, Input } from '@angular/core';

import { ScriptDto } from 'src/app/models/scripts/script-dto';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
    styleUrls: ['./script.component.sass'],
})
export class ScriptComponent {
    @Input() public script: ScriptDto;

    @Input() public avatarUrl: string = 'https://cdn-icons-png.flaticon.com/512/6596/6596121.png';

    // eslint-disable-next-line @typescript-eslint/no-useless-constructor, no-empty-function, @typescript-eslint/no-empty-function
    constructor() {}
}
