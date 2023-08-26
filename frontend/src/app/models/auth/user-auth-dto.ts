import { UserDto } from '../user/user-dto';

import { TokenDto } from './token-dto';

export interface UserAuthDto {
    user: UserDto;
    token: TokenDto;
}
