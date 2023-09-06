import { BranchDto } from '../branch/branch-dto';

import { ProjectDto } from './project-dto';

export interface NewProjectDto {
    project: ProjectDto;
    defaultBranch: BranchDto;
}
