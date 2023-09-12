import { Component, Input } from '@angular/core';

import { UserDto } from '../../../models/user/user-dto';

@Component({
    selector: 'app-user-card',
    templateUrl: './user-card.component.html',
    styleUrls: ['./user-card.component.sass'],
})
export class UserCardComponent {
    @Input() user: UserDto;
}
