import { User } from '../user/user';

import { AccessTokenDto } from './access-token-dto';

export interface UserAuthDto {
    user: User;
    token: AccessTokenDto;
}
