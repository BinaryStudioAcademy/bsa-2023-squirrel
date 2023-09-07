import { CreateBranchDto } from '../branch/create-branch-dto';

import { ProjectDto } from './project-dto';

export interface NewProjectDto {
    project: ProjectDto;
    defaultBranch: CreateBranchDto;
}
