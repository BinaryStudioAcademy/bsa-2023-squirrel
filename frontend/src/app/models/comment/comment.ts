import { User } from '../user/user';

export interface Comment {
    id: number;
    author: User;
    content: string;
    createdAt: Date;
    updatedAt: Date;
}
