import { UserDto } from 'src/app/models/user/user-dto';

export class UserPredicates {
    static findByFullNameOrUsernameOrEmail(item: UserDto, value: string) {
        return this.findByFullName(item, value) || this.findByEmail(item, value) || this.findByUsername(item, value);
    }

    static findByFullName(item: UserDto, value: string) {
        const lowerValue = value.toLowerCase();
        const fullName = `${item.firstName} ${item.lastName}`;

        return fullName.toLowerCase().indexOf(lowerValue) >= 0;
    }

    static findByUsername(item: UserDto, value: string) {
        const lowerValue = value.toLowerCase();

        return item.userName.toLowerCase().indexOf(lowerValue) >= 0;
    }

    static findByEmail(item: UserDto, value: string) {
        const lowerValue = value.toLowerCase();

        return item.email.indexOf(lowerValue) >= 0;
    }
}
