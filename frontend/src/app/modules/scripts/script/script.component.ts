import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

import { ScriptDto } from 'src/app/models/scripts/script-dto';

@Component({
    selector: 'app-script',
    templateUrl: './script.component.html',
    styleUrls: ['./script.component.sass'],
})
export class ScriptComponent {
    @Input() public script: ScriptDto;

    @Input() public avatarUrl: string = 'assets/profile_icon.svg';

    @Output() public delete = new EventEmitter();

    public trashIcon = faTrash;

    public deleteScript($event: Event) {
        $event.stopPropagation();
        this.delete.emit();
    }
}
