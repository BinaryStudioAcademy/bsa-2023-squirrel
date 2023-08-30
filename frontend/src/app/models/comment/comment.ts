import { UserDto } from '../user/user-dto';

export interface Comment {
    id: number;
    author: UserDto;
    content: string;
    createdAt: Date;
    updatedAt: Date;
}
