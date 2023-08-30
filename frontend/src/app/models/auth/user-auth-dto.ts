import { UserDto } from '../user/user-dto';

import { AccessTokenDto } from './access-token-dto';

export interface UserAuthDto {
    user: UserDto;
    token: AccessTokenDto;
}
