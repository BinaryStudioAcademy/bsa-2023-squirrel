import { Branch } from '../branch/branch';
import { Comment } from '../comment/comment';
import { User } from '../user/user';

export interface PullRequest {
    id: number;
    title: string;
    author: User;
    reviewers: User[];
    comments: Comment[];
    sourceBranch: Branch;
    destinationBranch: Branch;
    createdAt: Date;
    updatedAt: Date;
}
