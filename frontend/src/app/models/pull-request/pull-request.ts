import { Branch } from '../branch/branch';
import { User } from '../user/user';

export interface PullRequest {
    id: number;
    title: string;
    author: User;
    reviewers: User[];
    comments: Comment[];
    sourceBranch: Branch;
    destinationBranch: Branch;
    createdAt: string;
    updatedAt: string;
}
