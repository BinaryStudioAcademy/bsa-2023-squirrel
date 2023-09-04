import { DbEngine } from './db-engine';

export interface ProjectDto {
    id: number;
    name: string;
    engine: DbEngine;
}
