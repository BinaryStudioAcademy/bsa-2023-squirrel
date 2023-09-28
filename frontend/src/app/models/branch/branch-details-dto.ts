import { UserDto } from '../user/user-dto';

export interface BranchDetailsDto {
    id: number,
    name: string;
    isActive: boolean;
    lastUpdatedBy: UserDto;
    behind: number;
    ahead: number;
    createdAt: Date;
    updatedAt: Date;
}
