import { DbEngine } from './db-engine';

export interface ProjectDto {
    name: string;
    description: string | null;
    dbEngine: DbEngine;
}
