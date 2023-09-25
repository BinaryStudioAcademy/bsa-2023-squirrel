import { Component, Input } from '@angular/core';

import { ScriptErrorDto } from 'src/app/models/scripts/script-error-dto';

@Component({
    selector: 'app-script-error-result',
    templateUrl: './script-error-result.component.html',
    styleUrls: ['./script-error-result.component.sass'],
})
export class ScriptErrorResultComponent {
    @Input() public scriptError: ScriptErrorDto;

    @Input() public scriptTitle: string;
}
