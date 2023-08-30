import { User } from '../user/user-dto';

export interface Comment {
    id: number;
    author: User;
    content: string;
    createdAt: Date;
    updatedAt: Date;
}
