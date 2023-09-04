import { DbEngine } from './db-engine';

export interface ProjectDto {
    name: string;
    defaultBranchName: string;
    dbEngine: DbEngine;
}
