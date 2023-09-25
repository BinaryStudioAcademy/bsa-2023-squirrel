import { Component, Input } from '@angular/core';

import { ScriptResultDto } from 'src/app/models/scripts/script-result-dto';

@Component({
    selector: 'app-script-result',
    templateUrl: './script-result.component.html',
    styleUrls: ['./script-result.component.sass'],
})
export class ScriptResultComponent {
    @Input() public scriptResult: ScriptResultDto;

    @Input() public scriptTitle: string;
}
