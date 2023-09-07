import { Branch } from '../branch/branch';
import { Comment } from '../comment/comment';
import { UserDto } from '../user/user-dto';

export interface PullRequest {
    id: number;
    title: string;
    author: UserDto;
    reviewers: UserDto[];
    comments: Comment[];
    sourceBranch: Branch;
    destinationBranch: Branch;
    createdAt: Date;
    updatedAt: Date;
}
